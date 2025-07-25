using AspNetCorePills.Todo;
using System.Net.Http.Json;

namespace AspNetCorePills.Web.Blazor.Client;

public class TodoHttpService(HttpClient httpClient) : ITodoService
{
    public async Task AddItem(TodoItem item)
    {
        await httpClient.PostAsJsonAsync("api/todos", item);
    }

    public async Task DeleteItem(Guid todoId)
    {
        await httpClient.DeleteAsync($"api/todos/{todoId}");
    }

    public async Task<IEnumerable<TodoItem>> GetItems()
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<TodoItem>>("api/todos") ?? [];
    }

    public async Task UpdateItem(Guid todoId, TodoItem updatedItem)
    {
        await httpClient.PutAsJsonAsync($"api/todos/{todoId}", updatedItem);
    }
}
