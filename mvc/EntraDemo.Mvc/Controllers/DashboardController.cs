using EntraDemo.Mvc.Models;
using EntraDemo.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace EntraDemo.Mvc.Controllers;

/// <summary>
/// Signed-in area. [AuthorizeForScopes] catches "user interaction required" token
/// errors (e.g. the token cache was emptied by an app restart) and sends the user
/// through Entra sign-in again instead of failing.
/// </summary>
[AuthorizeForScopes(ScopeKeySection = "TodoApi:Scopes")]
public class DashboardController(TodoApiClient api) : Controller
{
    public async Task<IActionResult> Index()
    {
        Me? me = null;
        List<Todo> todos = [];
        string? profileError = null, todosError = null;

        try { me = await api.GetMeAsync(); }
        catch (Exception ex) when (IsApiFailure(ex)) { profileError = ex.Message; }

        try { todos = await api.GetTodosAsync(); }
        catch (Exception ex) when (IsApiFailure(ex)) { todosError = ex.Message; }

        return View(new DashboardViewModel
        {
            Me = me,
            Todos = todos,
            ProfileError = profileError,
            TodosError = TempData["TodosError"] as string ?? todosError,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public Task<IActionResult> Add(string? title) => Run(async () =>
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            await api.AddTodoAsync(title.Trim());
        }
    });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public Task<IActionResult> SetDone(Guid id, bool done) => Run(() => api.SetDoneAsync(id, done));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public Task<IActionResult> Delete(Guid id) => Run(() => api.DeleteTodoAsync(id));

    // Post/Redirect/Get: perform the change, then reload the dashboard.
    private async Task<IActionResult> Run(Func<Task> action)
    {
        try { await action(); }
        catch (Exception ex) when (IsApiFailure(ex)) { TempData["TodosError"] = ex.Message; }
        return RedirectToAction(nameof(Index));
    }

    // Token-acquisition exceptions are deliberately not caught so [AuthorizeForScopes] can handle them.
    private static bool IsApiFailure(Exception ex) => ex is ApiException or HttpRequestException;
}
