using AspNetCorePills.Todo;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePills.Web.MinimalApi;

public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/todos");
            //.RequireAuthorization();

        group.MapGet(
            "",
            (TodoService service) =>
            {
                var todos = service.GetItems();
                return Results.Ok(todos);
            }).WithName("GetTodos");

        group.MapGet(
            "{todoId:guid}",
            (Guid todoId, TodoService service) =>
            {
                var todo = service.GetItems().FirstOrDefault(item => item.Id == todoId);
                if (todo is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(todo);
            })
            .WithName("TodoDetail");

        group.MapPost(
            "",
            ([FromBody] TodoItem todo, TodoService service) =>
            {
                service.AddItem(todo);
                return Results.CreatedAtRoute("TodoDetail", new { todoId = todo.Id }, todo);
            }).AddEndpointFilter<ValidationFilter>()
            .WithName("CreateTodoItem");

        group.MapPut(
            "{todoId:guid}",
            ([FromBody] TodoItem updatedTodo, Guid todoId, TodoService service) =>
            {
                service.UpdateItem(todoId, updatedTodo);
                return Results.NoContent();
            })
            .WithName("UpdateTodoItem");

        group.MapDelete(
            "{todoId:guid}",
            (Guid todoId, TodoService service) =>
            {
                service.DeleteItem(todoId);
                return Results.NoContent();
            })
            .WithName("DeleteTodoItem");

        return app;
    }
}
