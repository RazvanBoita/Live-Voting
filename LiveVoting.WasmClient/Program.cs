using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LiveVoting.WasmClient;
using LiveVoting.WasmClient.Handlers;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var serverAddress = builder.Configuration["Server"];

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddTransient<AuthorizationMessageHandler>();

builder.Services.AddHttpClient("BackendAPI", client =>
{
    client.BaseAddress = new Uri(serverAddress);
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BackendAPI"));

await builder.Build().RunAsync();