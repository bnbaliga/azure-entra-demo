using EntraDemo.Mvc.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

var apiOptions = builder.Configuration.GetSection(TodoApiOptions.SectionName).Get<TodoApiOptions>() ?? new();
builder.Services.Configure<TodoApiOptions>(builder.Configuration.GetSection(TodoApiOptions.SectionName));

// Sign users in with OpenID Connect against Microsoft Entra ID (workforce or External ID),
// keep the session in an auth cookie, and acquire access tokens for the API server-side.
// Requesting the API scopes at sign-in means consent happens up front and the first
// token is already cached when the dashboard loads.
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi(apiOptions.Scopes)
    // In-memory is fine for a demo; use a distributed cache (Redis/SQL) when scaled out.
    .AddInMemoryTokenCaches();

builder.Services.AddHttpClient<TodoApiClient>(client =>
    client.BaseAddress = new Uri(apiOptions.BaseUrl.TrimEnd('/') + "/"));

builder.Services.AddControllersWithViews(options =>
{
    // Every page requires a signed-in user unless marked [AllowAnonymous].
    options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Exposed for integration tests.
public partial class Program;
