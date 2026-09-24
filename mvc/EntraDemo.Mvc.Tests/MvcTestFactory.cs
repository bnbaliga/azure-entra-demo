using System.Collections.Concurrent;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using EntraDemo.Mvc.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Abstractions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace EntraDemo.Mvc.Tests;

/// <summary>
/// Hosts the real MVC app with Entra and the downstream API replaced by fakes:
/// OIDC metadata is static (no network), token acquisition returns a fixed token,
/// and the API is a stub handler that records what the app sent.
/// </summary>
public class MvcTestFactory : WebApplicationFactory<Program>
{
    public const string ClientId = "aaaaaaaa-1111-2222-3333-444444444444";
    public const string ApiScope = "api://bbbbbbbb-1111-2222-3333-444444444444/access_as_user";
    public const string AuthorizeEndpoint = "https://contosodemo.ciamlogin.com/tid/oauth2/v2.0/authorize";
    public const string FakeToken = "fake-access-token";

    public StubApiHandler Api { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("AzureAd:Instance", "https://contosodemo.ciamlogin.com/");
        builder.UseSetting("AzureAd:TenantId", "tid");
        builder.UseSetting("AzureAd:ClientId", ClientId);
        builder.UseSetting("AzureAd:ClientSecret", "test-secret");
        builder.UseSetting("TodoApi:BaseUrl", "http://api.test");
        builder.UseSetting("TodoApi:Scopes:0", ApiScope);

        builder.ConfigureServices(services =>
        {
            var metadata = new OpenIdConnectConfiguration
            {
                Issuer = "https://contosodemo.ciamlogin.com/tid/v2.0",
                AuthorizationEndpoint = AuthorizeEndpoint,
                TokenEndpoint = "https://contosodemo.ciamlogin.com/tid/oauth2/v2.0/token",
                EndSessionEndpoint = "https://contosodemo.ciamlogin.com/tid/oauth2/v2.0/logout",
            };
            services.PostConfigure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Configuration = metadata;
                options.ConfigurationManager = new StaticConfigurationManager<OpenIdConnectConfiguration>(metadata);
            });

            // Requests carrying X-Test-User are treated as signed in.
            services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });
            services.PostConfigure<AuthenticationOptions>(o => o.DefaultAuthenticateScheme = TestAuthHandler.SchemeName);

            services.RemoveAll<IAuthorizationHeaderProvider>();
            services.AddSingleton<IAuthorizationHeaderProvider, FakeAuthorizationHeaderProvider>();

            services.AddHttpClient<TodoApiClient>().ConfigurePrimaryHttpMessageHandler(() => Api);
        });
    }
}

public class TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string SchemeName = "Test";
    public const string Header = "X-Test-User";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Header, out var name))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var identity = new ClaimsIdentity([new Claim("name", name.ToString()), new Claim("oid", "user-oid")], SchemeName);
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(identity), SchemeName)));
    }
}

public class FakeAuthorizationHeaderProvider : IAuthorizationHeaderProvider
{
    public List<string[]> RequestedScopes { get; } = [];

    public Task<string> CreateAuthorizationHeaderForUserAsync(IEnumerable<string> scopes,
        AuthorizationHeaderProviderOptions? options = null, ClaimsPrincipal? claimsPrincipal = null, CancellationToken cancellationToken = default)
    {
        RequestedScopes.Add(scopes.ToArray());
        return Task.FromResult($"Bearer {MvcTestFactory.FakeToken}");
    }

    public Task<string> CreateAuthorizationHeaderForAppAsync(string scopes,
        AuthorizationHeaderProviderOptions? downstreamApiOptions = null, CancellationToken cancellationToken = default) =>
        throw new NotSupportedException();

    public Task<string> CreateAuthorizationHeaderAsync(IEnumerable<string> scopes,
        AuthorizationHeaderProviderOptions? options = null, ClaimsPrincipal? claimsPrincipal = null, CancellationToken cancellationToken = default) =>
        CreateAuthorizationHeaderForUserAsync(scopes, options, claimsPrincipal, cancellationToken);
}

/// <summary>Stands in for EntraDemo.Api: rejects calls without a bearer token, like the real one.</summary>
public class StubApiHandler : HttpMessageHandler
{
    public ConcurrentQueue<(HttpMethod Method, string Path, string? Authorization, string? Body)> Requests { get; } = new();

    /// <summary>When set, every authenticated call returns this status (to simulate API errors).</summary>
    public HttpStatusCode? ForceStatus { get; set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var body = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
        var auth = request.Headers.Authorization?.ToString();
        var path = request.RequestUri!.AbsolutePath;
        Requests.Enqueue((request.Method, path, auth, body));

        if (auth is null)
        {
            var unauthorized = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            unauthorized.Headers.WwwAuthenticate.ParseAdd("Bearer");
            return unauthorized;
        }
        if (ForceStatus is { } status)
        {
            var failed = new HttpResponseMessage(status);
            failed.Headers.WwwAuthenticate.ParseAdd("Bearer error=\"invalid_token\"");
            return failed;
        }

        return (request.Method.Method, path) switch
        {
            ("GET", "/api/me") => Json("""
                {"name":"Ada Lovelace","username":"ada@example.com","objectId":"user-oid","tenantId":"tid",
                 "scopes":["access_as_user"],"claims":[{"type":"oid","value":"user-oid"}]}
                """),
            ("GET", "/api/todos") => Json("""
                [{"id":"6f1c2c1e-0000-0000-0000-000000000001","title":"Existing task","done":false,"createdAt":"2026-01-01T00:00:00Z"}]
                """),
            ("POST", "/api/todos") => Json("""
                {"id":"6f1c2c1e-0000-0000-0000-000000000002","title":"New","done":false,"createdAt":"2026-01-01T00:00:00Z"}
                """, HttpStatusCode.Created),
            ("PATCH", _) => Json("""
                {"id":"6f1c2c1e-0000-0000-0000-000000000001","title":"Existing task","done":true,"createdAt":"2026-01-01T00:00:00Z"}
                """),
            ("DELETE", _) => new HttpResponseMessage(HttpStatusCode.NoContent),
            _ => new HttpResponseMessage(HttpStatusCode.NotFound),
        };
    }

    private static HttpResponseMessage Json(string json, HttpStatusCode status = HttpStatusCode.OK) =>
        new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
}
