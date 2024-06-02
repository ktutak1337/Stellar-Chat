using StellarChat.Shared.Contracts.Settings;

namespace StellarChat.Client.Web.Services.Settings;

internal interface ISettingsService
{
    private const string SettingsKey = "app-settings";

    ValueTask<AppSettingsResponse> GetSettingsAsync(string key = SettingsKey);
    ValueTask UpdateProfileAsync(string name, string avatarUrl, string description, string key = SettingsKey);
}
