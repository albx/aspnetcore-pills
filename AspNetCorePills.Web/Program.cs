using AspNetCorePills.Web;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//var configurationObject = new ConfigurationObject();
//builder.Configuration.GetSection("ConfigurationObject").Bind(configurationObject);

//Servizi

builder.Services.Configure<ConfigurationObject>(options =>
{
    options.Name = builder.Configuration["ConfigurationObject:Name"]!;
    options.Value = builder.Configuration["ConfigurationObject:Value"]!;
});

//Singleton
//builder.Services.AddSingleton<MyService>();

//Scoped
//builder.Services.AddScoped<MyService>();

//Transient
//builder.Services.AddTransient<MyService>();

//Keyed service
//builder.Services.AddKeyedScoped<MyService>("service");

var app = builder.Build();

//Middleware

app.UseMyMiddleware();

app.MapGet(
    "/", 
    (IOptions<ConfigurationObject> configurationOptions) =>
    {
        return $"Hello World! {configurationOptions.Value.Name} - {configurationOptions.Value.Value}";
    });

app.Run();


record ConfigurationObject
{
    public string Name { get; set; }

    public string Value { get; set; }
}