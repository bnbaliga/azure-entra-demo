namespace EntraDemo.Mvc.Models;

// Shapes returned by EntraDemo.Api.

public record Claim(string Type, string Value);

public record Me(
    string? Name,
    string? Username,
    string? ObjectId,
    string? TenantId,
    string[] Scopes,
    Claim[] Claims);

public record Todo(Guid Id, string Title, bool Done, DateTimeOffset CreatedAt);
