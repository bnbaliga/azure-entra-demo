using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace EntraDemo.Api.Tests;

/// <summary>
/// Hosts the real API pipeline but swaps Entra's signing keys for a local test key,
/// so tests can mint tokens without calling Microsoft Entra ID.
/// </summary>
public class TestAuthFactory : WebApplicationFactory<Program>
{
    public const string TenantId = "11111111-1111-1111-1111-111111111111";
    public const string ClientId = "22222222-2222-2222-2222-222222222222";
    public const string Issuer = $"https://login.microsoftonline.com/{TenantId}/v2.0";

    private static readonly SymmetricSecurityKey SigningKey =
        new(Encoding.UTF8.GetBytes("test-signing-key-that-is-long-enough-for-hs256"));

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("AzureAd:TenantId", TenantId);
        builder.UseSetting("AzureAd:ClientId", ClientId);

        builder.ConfigureServices(services =>
            services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Configuration = new OpenIdConnectConfiguration { Issuer = Issuer };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Issuer,
                    ValidAudiences = [ClientId, $"api://{ClientId}"],
                    IssuerSigningKey = SigningKey,
                };
            }));
    }

    public static string CreateToken(
        string objectId = "33333333-3333-3333-3333-333333333333",
        string? scope = "access_as_user",
        string audience = ClientId,
        DateTime? expires = null)
    {
        var claims = new List<Claim>
        {
            new("oid", objectId),
            new("tid", TenantId),
            new("name", "Test User"),
            new("preferred_username", "test.user@example.com"),
        };
        if (scope is not null)
        {
            claims.Add(new Claim("scp", scope));
        }

        var expiry = expires ?? DateTime.UtcNow.AddHours(1);
        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: audience,
            claims: claims,
            notBefore: expiry.AddHours(-2),
            expires: expiry,
            signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
