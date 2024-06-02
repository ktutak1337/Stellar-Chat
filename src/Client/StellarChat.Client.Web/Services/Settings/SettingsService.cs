using StellarChat.Shared.Contracts.Settings;
using System.Net.Http.Json;

namespace StellarChat.Client.Web.Services.Settings;

public class SettingsService(IHttpClientFactory httpClientFactory) : ISettingsService
{
    private const string SettingsKey = "app-settings";
    private const string HttpClientName = "WebAPI";

    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async ValueTask<AppSettingsResponse> GetSettingsAsync(string key = SettingsKey)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var result = await httpClient.GetFromJsonAsync<AppSettingsResponse>($"/settings/{key}");

        return result!;
    }

    public async ValueTask<HttpResponseMessage> UpdateProfileAsync(string name, string avatarUrl, string description, string key = SettingsKey)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var payload = new UpdateProfileRequest(key, name, avatarUrl, description);

        return await httpClient.PutAsJsonAsync($"/settings/{key}/profile", payload);
    }
}
