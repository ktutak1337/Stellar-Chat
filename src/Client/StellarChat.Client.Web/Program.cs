using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using StellarChat.Client.Web;
using StellarChat.Client.Web.Services.Actions;
using StellarChat.Client.Web.Services.Assistants;
using StellarChat.Client.Web.Services.Chat;
using StellarChat.Client.Web.Services.Models;
using StellarChat.Client.Web.Services.Settings;
using StellarChat.Client.Web.Services.Storage;
using StellarChat.Client.Web.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.TryAddSingleton(TimeProvider.System);

var apiUrl = builder.Configuration["api:api_url"];

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri(apiUrl!);
});

builder.Services.AddScoped<ChatState>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<IAssistantService, AssistantService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IAvailableModelsService, AvailableModelsService>();

await builder.Build().RunAsync();
