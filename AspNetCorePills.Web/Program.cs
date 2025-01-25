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

app.MapGet(
    "/", 
    ([FromKeyedServices("service")]MyService service) =>
    {
        return $"Hello World! {service.GetValue()}";
    });

app.Run();

#region Service
class MyService
{
    private static int value = 0;

    public MyService()
    {
        value++;
    }

    public int GetValue() => value;
}
#endregion