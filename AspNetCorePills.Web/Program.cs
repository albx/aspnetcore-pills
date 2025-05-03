using AspNetCorePills.Web;

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

//builder.Services.Configure<ConfigurationObject>(options =>
//{
//    options.Name = builder.Configuration["ConfigurationObject:Name"]!;
//    options.Value = builder.Configuration["ConfigurationObject:Value"]!;
//});

//Singleton
//builder.Services.AddSingleton<MyService>();

//Scoped
builder.Services.AddScoped<MyService>();

//Transient
//builder.Services.AddTransient<MyService>();

//Keyed service
//builder.Services.AddKeyedScoped<MyService>("service");

var app = builder.Build();

//Middleware

app.UseMyMiddleware();

app.MapGet(
    "/", 
    (ILogger<Program> logger, MyService service) =>
    {
        logger.LogInformation("Hello World!");

        return $"Hello World! {service.GetValue()}";
    });

app.Run();
