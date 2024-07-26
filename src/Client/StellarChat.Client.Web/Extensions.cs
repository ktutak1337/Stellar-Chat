using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using StellarChat.Client.Web.Services.Actions;
using StellarChat.Client.Web.Services.Assistants;
using StellarChat.Client.Web.Services.Chat;
using StellarChat.Client.Web.Services.Models;
using StellarChat.Client.Web.Services.Settings;
using StellarChat.Client.Web.Services.Storage;
using StellarChat.Client.Web.Shared.Http;
using StellarChat.Client.Web.State;
using StellarChat.Shared.Contracts.Settings;

namespace StellarChat.Client.Web;

internal static class Extensions
{
    public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.TryAddSingleton(TimeProvider.System);

        builder.Services.AddScoped<IRestHttpClient, RestHttpClient>();
        builder.Services.AddScoped<ChatState>();
        builder.Services.AddScoped<IChatService, ChatService>();
        builder.Services.AddScoped<IActionService, ActionService>();
        builder.Services.AddScoped<IAssistantService, AssistantService>();
        builder.Services.AddScoped<ISettingsService, SettingsService>();
        builder.Services.AddScoped<IStorageService, StorageService>();
        builder.Services.AddScoped<IModelCatalogService, ModelCatalogService>();

        builder.Services.AddMudServices();

        return builder;
    }

    public static Integration FetchIntegrationSettings(this AppSettingsResponse settings, string providerName) 
        => settings.Integrations.First(x => x.Name.Equals(providerName, StringComparison.InvariantCultureIgnoreCase));
}
