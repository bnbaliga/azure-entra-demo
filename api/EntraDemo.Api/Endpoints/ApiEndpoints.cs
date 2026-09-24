using System.Security.Claims;
using EntraDemo.Api.Models;
using EntraDemo.Api.Services;
using Microsoft.Identity.Web;

namespace EntraDemo.Api.Endpoints;

public static class ApiEndpoints
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("/api");

        // Public endpoint: handy for checking the API is up. Everything else
        // falls under the fallback policy and requires a valid Entra token.
        api.MapGet("/health", () => Results.Ok(new { status = "ok", time = DateTimeOffset.UtcNow }))
            .AllowAnonymous();

        // Returns what the API learned about the caller from the validated access token.
        api.MapGet("/me", (ClaimsPrincipal user) => Results.Ok(new
        {
            name = user.FindFirstValue("name"),
            username = user.FindFirstValue("preferred_username") ?? user.FindFirstValue(ClaimTypes.Email),
            objectId = user.GetObjectId(),
            tenantId = user.GetTenantId(),
            scopes = user.FindFirstValue(ClaimConstants.Scp)?.Split(' ') ?? [],
            claims = user.Claims.Select(c => new { c.Type, c.Value }),
        }));

        var todos = api.MapGroup("/todos");

        todos.MapGet("/", (ClaimsPrincipal user, TodoStore store) =>
            Results.Ok(store.List(UserId(user))));

        todos.MapPost("/", (CreateTodoRequest request, ClaimsPrincipal user, TodoStore store) =>
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    ["title"] = ["Title is required."],
                });
            }

            var item = store.Add(UserId(user), request.Title.Trim());
            return Results.Created($"/api/todos/{item.Id}", item);
        });

        todos.MapPatch("/{id:guid}", (Guid id, UpdateTodoRequest request, ClaimsPrincipal user, TodoStore store) =>
            store.Update(UserId(user), id, request) is { } item ? Results.Ok(item) : Results.NotFound());

        todos.MapDelete("/{id:guid}", (Guid id, ClaimsPrincipal user, TodoStore store) =>
            store.Remove(UserId(user), id) ? Results.NoContent() : Results.NotFound());
    }

    // The oid claim is the user's immutable id across all apps in the tenant.
    private static string UserId(ClaimsPrincipal user) =>
        user.GetObjectId() ?? throw new InvalidOperationException("Token has no oid claim.");
}
