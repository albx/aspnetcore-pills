namespace AspNetCorePills.Todo;

public class TodoService
{
    public static List<TodoItem> Items { get; } = [
        new TodoItem { Id = Guid.NewGuid(), Title = "Learn C#" },
        new TodoItem { Id = Guid.NewGuid(), Title = "Build a web app" },
        new TodoItem { Id = Guid.NewGuid(), Title = "Deploy to production" },
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
}
