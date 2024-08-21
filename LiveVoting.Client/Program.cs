using Blazored.LocalStorage;
using LiveVoting.Client.Components;
using LiveVoting.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthorizationMessageHandler>();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["Server"]) ?? throw new UriFormatException() });

builder.Services.AddHttpClient("MyApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Server"]) ?? throw new UriFormatException();
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MyApi"));


builder.Services.AddBlazoredLocalStorage();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();