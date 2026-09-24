using System.Collections.Concurrent;
using EntraDemo.Api.Models;

namespace EntraDemo.Api.Services;

/// <summary>
/// In-memory, per-user todo storage. Items are partitioned by the caller's
/// Entra object id (oid claim), so users can only ever see their own data.
/// Swap for a real database in production.
/// </summary>
public class TodoStore
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<Guid, TodoItem>> _items = new();

    private ConcurrentDictionary<Guid, TodoItem> ForUser(string userId) =>
        _items.GetOrAdd(userId, _ => new ConcurrentDictionary<Guid, TodoItem>());

    public IEnumerable<TodoItem> List(string userId) =>
        ForUser(userId).Values.OrderBy(t => t.CreatedAt);

    public TodoItem Add(string userId, string title)
    {
        var item = new TodoItem(Guid.NewGuid(), title, false, DateTimeOffset.UtcNow);
        ForUser(userId)[item.Id] = item;
        return item;
    }

    public TodoItem? Update(string userId, Guid id, UpdateTodoRequest update)
    {
        var items = ForUser(userId);
        if (!items.TryGetValue(id, out var existing))
        {
            return null;
        }

        var updated = existing with
        {
            Title = string.IsNullOrWhiteSpace(update.Title) ? existing.Title : update.Title.Trim(),
            Done = update.Done ?? existing.Done,
        };
        items[id] = updated;
        return updated;
    }

    public bool Remove(string userId, Guid id) => ForUser(userId).TryRemove(id, out _);
}
