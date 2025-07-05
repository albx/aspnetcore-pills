namespace AspNetCorePills.Todo;

public class TodoService
{
    public static List<TodoItem> Items { get; } = [
        new TodoItem { Id = Guid.Parse("40570B4B-5E0D-4CA5-92F1-37F4379F9C0E"), Title = "Learn C#" },
        new TodoItem { Id = Guid.Parse("DB1A64B6-BC5D-4A46-9DA3-8A18CFC1376C"), Title = "Build a web app" },
        new TodoItem { Id = Guid.Parse("6B302A7E-8501-495B-832F-34623E6CDA0B"), Title = "Deploy to production" },
    ];

    public IEnumerable<TodoItem> GetItems()
    {
        return Items.OrderBy(item => item.Title);
    }

    public void AddItem(TodoItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        item.Id = Guid.NewGuid();
        Items.Add(item);
    }

    public void UpdateItem(Guid todoId, TodoItem updatedItem)
    {
        ArgumentNullException.ThrowIfNull(updatedItem);
        var item = Items.FirstOrDefault(i => i.Id == todoId);
        if (item is null)
        {
            throw new KeyNotFoundException($"Todo item with ID {todoId} not found.");
        }
        item.Title = updatedItem.Title;
    }

    public void DeleteItem(Guid todoId)
    {
        var item = Items.FirstOrDefault(i => i.Id == todoId);
        if (item is null)
        {
            throw new KeyNotFoundException($"Todo item with ID {todoId} not found.");
        }
        Items.Remove(item);
    }
}
