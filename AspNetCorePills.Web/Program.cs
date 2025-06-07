using AspNetCorePills.Web;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//var configurationObject = new ConfigurationObject();
//builder.Configuration.GetSection("ConfigurationObject").Bind(configurationObject);

//Servizi

builder.Services.AddLogging(loggingBuilder =>
{
    //loggingBuilder.ClearProviders();
    loggingBuilder.AddMyLogger(options =>
    {
        options.FilePath = "Logs";
    });
});

builder.Services.AddRazorPages();

builder.Services.Configure<ConfigurationObject>(options =>
{
    options.Name = builder.Configuration["ConfigurationObject:Name"]!;
    options.Value = builder.Configuration["ConfigurationObject:Value"]!;
});

//Scoped
builder.Services.AddScoped<MyService>();

var app = builder.Build();

//Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/StaticFiles",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
             "Cache-Control", $"public, max-age=3600");
    }
});

app.UseMyMiddleware();

app.MapRazorPages();

//app.MapGet(
//    "/", 
//    (ILogger<Program> logger, MyService service) =>
//    {
//        logger.LogInformation("Hello World!");

//        return $"Hello World! {service.GetValue()}";
//    });

app.Run();
