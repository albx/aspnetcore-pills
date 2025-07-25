
namespace AspNetCorePills.Todo;

public interface ITodoService
{
    Task AddItem(TodoItem item);
    Task DeleteItem(Guid todoId);
    Task<IEnumerable<TodoItem>> GetItems();
    Task UpdateItem(Guid todoId, TodoItem updatedItem);
}