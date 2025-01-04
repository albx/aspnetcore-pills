var builder = WebApplication.CreateBuilder(args);

//Servizi

var app = builder.Build();

//Middleware

app.MapGet("/", () => "Hello World!");

app.Run();
