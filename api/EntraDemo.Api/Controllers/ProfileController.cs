using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace EntraDemo.Api.Controllers;

[ApiController]
[Route("api/me")]
public class ProfileController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        name = User.FindFirstValue("name"),
        username = User.FindFirstValue("preferred_username") ?? User.FindFirstValue(ClaimTypes.Email),
        objectId = User.GetObjectId(),
        tenantId = User.GetTenantId(),
        scopes = User.FindFirstValue(ClaimConstants.Scp)?.Split(' ') ?? [],
        claims = User.Claims.Select(c => new { c.Type, c.Value }),
    });
}
