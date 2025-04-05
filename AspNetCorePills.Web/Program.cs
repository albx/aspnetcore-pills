using AspNetCorePills.Web;

var builder = WebApplication.CreateBuilder(args);

//Servizi

//Singleton
//builder.Services.AddSingleton<MyService>();

//Scoped
//builder.Services.AddScoped<MyService>();

//Transient
//builder.Services.AddTransient<MyService>();

//Keyed service
builder.Services.AddKeyedScoped<MyService>("service");

var app = builder.Build();

//Middleware
//app.Use(async (context, next) =>
//{
//    if (context.Request.Headers["X-MyHeader"].Contains("test"))
//    {
//        await context.Response.WriteAsync("Header test found!");
//        return;
//    }

//    await next(context);
//});

app.UseMyMiddleware();

app.MapGet(
    "/", 
    ([FromKeyedServices("service")]MyService service) =>
    {
        return $"Hello World! {service.GetValue()}";
    });

app.Run();

class MyMiddleware
{
    private readonly RequestDelegate _next;
    public MyMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers["X-MyHeader"].Contains("test"))
        {
            await context.Response.WriteAsync("Header test found!");
            return;
        }

        await _next(context);
    }
}

static class MyMiddlewareExtensions
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyMiddleware>();
    }
}