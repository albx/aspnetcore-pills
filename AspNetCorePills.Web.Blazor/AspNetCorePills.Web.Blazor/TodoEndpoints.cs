using AspNetCorePills.Todo;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePills.Web.Blazor;

public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/todos");

        group.MapGet(
            "",
            async (ITodoService service) =>
            {
                var todos = await service.GetItems();
                return Results.Ok(todos);
            }).WithName("GetTodos");

        group.MapGet(
            "{todoId:guid}",
            async (Guid todoId, ITodoService service) =>
            {
                var todo = (await service.GetItems()).FirstOrDefault(item => item.Id == todoId);
                if (todo is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(todo);
            })
            .WithName("TodoDetail");

        group.MapPost(
            "",
            async ([FromBody] TodoItem todo, ITodoService service) =>
            {
                await service.AddItem(todo);
                return Results.CreatedAtRoute("TodoDetail", new { todoId = todo.Id }, todo);
            })
            .WithName("CreateTodoItem");

        group.MapPut(
            "{todoId:guid}",
            async ([FromBody] TodoItem updatedTodo, Guid todoId, ITodoService service) =>
            {
                await service.UpdateItem(todoId, updatedTodo);
                return Results.NoContent();
            })
            .WithName("UpdateTodoItem");

        group.MapDelete(
            "{todoId:guid}",
            async (Guid todoId, ITodoService service) =>
            {
                await service.DeleteItem(todoId);
                return Results.NoContent();
            })
            .WithName("DeleteTodoItem");

        return app;
    }
}
