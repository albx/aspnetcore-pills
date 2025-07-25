using AspNetCorePills.Todo;

namespace AspNetCorePills.Web.Blazor.Client.Components;

public partial class TodoListManager(ITodoService todoService)
{
    public ITodoService TodoService { get; } = todoService;

    private bool loading = false;

    private IEnumerable<TodoItem> items = [];

    protected override async Task OnInitializedAsync()
    {
        loading = true;

        try
        {
            items = await TodoService.GetItems();
        }
        finally
        {
            loading = false;
        }
    }

    private async Task RefreshItemsAsync(TodoItem itemAdded)
    {
        items = await TodoService.GetItems();
    }
}
