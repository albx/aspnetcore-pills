using AspNetCorePills.Todo;
using AspNetCorePills.Web.Blazor.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<ITodoService, TodoHttpService>(
    client => client.BaseAddress = new(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();
