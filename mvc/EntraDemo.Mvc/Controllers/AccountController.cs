using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntraDemo.Mvc.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private const string AfterSignIn = "/Dashboard";

    /// <summary>Redirects to Entra's sign-in page (OIDC authorization code flow).</summary>
    public IActionResult SignIn() =>
        Challenge(new AuthenticationProperties { RedirectUri = AfterSignIn }, OpenIdConnectDefaults.AuthenticationScheme);

    /// <summary>
    /// Redirects straight to Entra's sign-up page via prompt=create. Requires a tenant with
    /// self-service sign-up (Entra External ID with a "Sign up and sign in" user flow
    /// linked to this app); workforce tenants reject it.
    /// </summary>
    public IActionResult SignUp() =>
        Challenge(
            new OpenIdConnectChallengeProperties { RedirectUri = AfterSignIn, Prompt = "create" },
            OpenIdConnectDefaults.AuthenticationScheme);

    /// <summary>Clears the local session cookie and signs out of Entra.</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public new IActionResult SignOut() =>
        SignOut(
            new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
}
