using AspNetCorePills.Todo;
using AspNetCorePills.Web.MinimalApi;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TodoService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapTodoEndpoints();

app.Run();

public class ValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        var todoItem = context.GetArgument<TodoItem>(0);
        if (string.IsNullOrWhiteSpace(todoItem.Title))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(todoItem.Title)] = ["The title is required."]
            });
        }

        return await next(context);
    }
}
