using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Settings;

namespace StellarChat.Client.Web.Services.Settings;

public class SettingsService(IRestHttpClient httpClient) : ISettingsService
{
    private const string SettingsKey = "app-settings";
    private readonly IRestHttpClient _httpClient = httpClient;

    public async ValueTask<ApiResponse<AppSettingsResponse>> GetSettingsAsync(string key = SettingsKey) 
        => await _httpClient.GetAsync<AppSettingsResponse>($"/settings/{key}");

    public async ValueTask<ApiResponse> UpdateProfileAsync(string name, string avatarUrl, string description, string key = SettingsKey)
    {
        var payload = new UpdateProfileRequest(key, name, avatarUrl, description);

        var response = await _httpClient.PutAsync($"/settings/{key}/profile", payload);

        return response;
    }
}
