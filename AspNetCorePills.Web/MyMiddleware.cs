namespace AspNetCorePills.Web;

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