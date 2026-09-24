namespace EntraDemo.Api.Models;

public record TodoItem(Guid Id, string Title, bool Done, DateTimeOffset CreatedAt);

public record CreateTodoRequest(string Title);

public record UpdateTodoRequest(string? Title, bool? Done);
