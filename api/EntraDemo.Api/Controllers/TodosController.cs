using EntraDemo.Api.Models;
using EntraDemo.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace EntraDemo.Api.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController(TodoStore store) : ControllerBase
{
    [HttpGet]
    public IActionResult List() => Ok(store.List(UserId()));

    [HttpPost]
    public IActionResult Create(CreateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            ModelState.AddModelError("title", "Title is required.");
            return ValidationProblem(ModelState);
        }

        var item = store.Add(UserId(), request.Title.Trim());
        return Created($"/api/todos/{item.Id}", item);
    }

    [HttpPatch("{id:guid}")]
    public IActionResult Update(Guid id, UpdateTodoRequest request) =>
        store.Update(UserId(), id, request) is { } item ? Ok(item) : NotFound();

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) =>
        store.Remove(UserId(), id) ? NoContent() : NotFound();

    private string UserId() =>
        User.GetObjectId() ?? throw new InvalidOperationException("Token has no oid claim.");
}
