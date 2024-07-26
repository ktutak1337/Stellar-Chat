using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Settings;

namespace StellarChat.Client.Web.Services.Settings;

internal interface ISettingsService
{
    private const string SettingsKey = "app-settings";

    ValueTask<ApiResponse<AppSettingsResponse>> GetSettingsAsync(string key = SettingsKey);
    ValueTask<ApiResponse> UpdateProfileAsync(string name, string avatarUrl, string description, string key = SettingsKey);
    ValueTask<ApiResponse> UpdateIntegrationsAsync(List<Integration> Integrations, string key = SettingsKey);
}
