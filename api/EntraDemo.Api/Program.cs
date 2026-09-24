using EntraDemo.Api.Endpoints;
using EntraDemo.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Validate incoming bearer tokens issued by Microsoft Entra ID (workforce tenant
// or Entra External ID). Issuer, audience, signature and lifetime are checked
// against the tenant's OpenID Connect metadata, configured in the "AzureAd" section.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Space-separated list of delegated scopes, any one of which grants access.
var requiredScopes = builder.Configuration["AzureAd:Scopes"]?
    .Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];

builder.Services.AddAuthorization(options =>
{
    // Every endpoint requires an authenticated caller holding the API scope
    // unless it explicitly opts out with AllowAnonymous().
    var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser();
    if (requiredScopes.Length > 0)
    {
        policy.RequireScope(requiredScopes);
    }
    options.FallbackPolicy = policy.Build();
});

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()
        // Lets the SPA read why a request was rejected (e.g. invalid_token, insufficient_scope).
        .WithExposedHeaders("WWW-Authenticate")));

builder.Services.AddSingleton<TodoStore>();

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints();

app.Run();

// Exposed for integration tests.
public partial class Program;
