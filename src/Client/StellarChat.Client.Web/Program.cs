using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using StellarChat.Client.Web;
using StellarChat.Client.Web.Services.Chat;
using StellarChat.Client.Web.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7057");
});

builder.Services.AddScoped<ChatState>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IChatService, ChatService>();

await builder.Build().RunAsync();
