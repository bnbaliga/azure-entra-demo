using System.Diagnostics;
using EntraDemo.Mvc.Models;
using EntraDemo.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntraDemo.Mvc.Controllers;

[AllowAnonymous]
public class HomeController(IConfiguration configuration, TodoApiClient api) : Controller
{
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View(new LandingViewModel
        {
            MissingSettings = SetupCheck.MissingSettings(configuration),
            ProbeResult = TempData["ProbeResult"] as string,
        });
    }

    /// <summary>Calls the API without a token to show that it is rejected.</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProbeApi()
    {
        try
        {
            var result = await api.CallAnonymouslyAsync("api/me");
            TempData["ProbeResult"] = result.StartsWith("401")
                ? $"Rejected as expected → {result}"
                : $"Unexpected: the API returned {result}";
        }
        catch (HttpRequestException ex)
        {
            TempData["ProbeResult"] = $"Could not reach the API: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
