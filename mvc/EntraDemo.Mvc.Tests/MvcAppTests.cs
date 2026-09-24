using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EntraDemo.Mvc.Tests;

public partial class MvcAppTests : IDisposable
{
    private readonly MvcTestFactory _factory = new();

    public void Dispose() => _factory.Dispose();

    private HttpClient Client(string? user = null)
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("https://localhost"),
        });
        if (user is not null)
        {
            client.DefaultRequestHeaders.Add(TestAuthHandler.Header, user);
        }
        return client;
    }

    [Fact]
    public async Task Landing_IsPublic_AndOffersSignInAndSignUp()
    {
        var html = await Client().GetStringAsync("/");
        Assert.Contains("/Account/SignIn", html);
        Assert.Contains("/Account/SignUp", html);
        Assert.DoesNotContain("Configuration needed", html);
    }

    [Fact]
    public async Task Dashboard_WhenAnonymous_RedirectsToEntraWithAuthCodeFlow()
    {
        var response = await Client().GetAsync("/Dashboard");

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        var location = response.Headers.Location!;
        Assert.StartsWith(MvcTestFactory.AuthorizeEndpoint, location.ToString());

        var query = HttpUtility.ParseQueryString(location.Query);
        Assert.Equal(MvcTestFactory.ClientId, query["client_id"]);
        Assert.Equal("code", query["response_type"]);
        Assert.Equal("https://localhost/signin-oidc", query["redirect_uri"]);
        Assert.Contains(MvcTestFactory.ApiScope, query["scope"]!.Split(' '));
        Assert.Contains("openid", query["scope"]!.Split(' '));
        Assert.NotNull(query["code_challenge"]);
        Assert.Null(query["prompt"]);
        Assert.Empty(_factory.Api.Requests);
    }

    [Fact]
    public async Task SignUp_RedirectsToEntraWithPromptCreate()
    {
        var response = await Client().GetAsync("/Account/SignUp");

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        var query = HttpUtility.ParseQueryString(response.Headers.Location!.Query);
        Assert.Equal("create", query["prompt"]);
        Assert.Equal(MvcTestFactory.ClientId, query["client_id"]);
    }

    [Fact]
    public async Task SignedInUser_IsRedirectedFromLandingToDashboard()
    {
        var response = await Client("Ada Lovelace").GetAsync("/");
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/Dashboard", response.Headers.Location!.ToString());
    }

    [Fact]
    public async Task Dashboard_CallsApiWithBearerToken_AndRendersResults()
    {
        var html = await Client("Ada Lovelace").GetStringAsync("/Dashboard");

        Assert.Contains("ada@example.com", html);
        Assert.Contains("Existing task", html);
        Assert.Contains(_factory.Api.Requests, r => r.Path == "/api/me");
        Assert.Contains(_factory.Api.Requests, r => r.Path == "/api/todos");
        Assert.All(_factory.Api.Requests, r => Assert.Equal($"Bearer {MvcTestFactory.FakeToken}", r.Authorization));
    }

    [Fact]
    public async Task AddTodo_PostsToApi_ThenRedirectsBackToDashboard()
    {
        var client = Client("Ada Lovelace");
        var token = AntiforgeryToken(await client.GetStringAsync("/Dashboard"));

        var response = await client.PostAsync("/Dashboard/Add", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["title"] = "  Buy milk  ",
            ["__RequestVerificationToken"] = token,
        }));

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/Dashboard", response.Headers.Location!.ToString());
        var post = Assert.Single(_factory.Api.Requests, r => r.Method == HttpMethod.Post);
        Assert.Equal("""{"title":"Buy milk"}""", post.Body);
        Assert.Equal($"Bearer {MvcTestFactory.FakeToken}", post.Authorization);
    }

    [Fact]
    public async Task Post_WithoutAntiforgeryToken_IsRejected()
    {
        var response = await Client("Ada Lovelace").PostAsync("/Dashboard/Add",
            new FormUrlEncodedContent(new Dictionary<string, string> { ["title"] = "x" }));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Empty(_factory.Api.Requests);
    }

    [Fact]
    public async Task Dashboard_WhenApiRejectsToken_ShowsErrorInsteadOfCrashing()
    {
        _factory.Api.ForceStatus = HttpStatusCode.Unauthorized;

        var response = await Client("Ada Lovelace").GetAsync("/Dashboard");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("401 Unauthorized", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task ProbeApi_CallsApiWithoutToken_AndShowsRejection()
    {
        var client = Client();
        var token = AntiforgeryToken(await client.GetStringAsync("/"));

        var response = await client.PostAsync("/Home/ProbeApi",
            new FormUrlEncodedContent(new Dictionary<string, string> { ["__RequestVerificationToken"] = token }));
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

        var html = WebUtility.HtmlDecode(await client.GetStringAsync("/"));
        Assert.Contains("Rejected as expected → 401 Unauthorized: Bearer", html);
        var call = Assert.Single(_factory.Api.Requests);
        Assert.Null(call.Authorization);
    }

    private static string AntiforgeryToken(string html) =>
        AntiforgeryRegex().Match(html).Groups[1].Value;

    [GeneratedRegex("name=\"__RequestVerificationToken\" type=\"hidden\" value=\"([^\"]+)\"")]
    private static partial Regex AntiforgeryRegex();
}
