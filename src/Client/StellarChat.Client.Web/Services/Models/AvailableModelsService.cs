using StellarChat.Shared.Contracts.Models;
using System.Net.Http.Json;

namespace StellarChat.Client.Web.Services.Models;

public class AvailableModelsService : IAvailableModelsService
{
    private const string HttpClientName = "WebAPI";

    private readonly IHttpClientFactory _httpClientFactory;

    public AvailableModelsService(IHttpClientFactory httpClientFactory) 
        => _httpClientFactory = httpClientFactory;

    public async ValueTask<IEnumerable<AvailableModelsResponse>> BrowseAvailableModels()
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var response = await httpClient.GetFromJsonAsync<IEnumerable<AvailableModelsResponse>>($"/models");

        return response!;
    }
}
