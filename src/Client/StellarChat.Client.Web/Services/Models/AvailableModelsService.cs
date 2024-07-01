using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Models;

namespace StellarChat.Client.Web.Services.Models;

public class AvailableModelsService(IRestHttpClient httpClient) : IAvailableModelsService
{
    private readonly IRestHttpClient _httpClient = httpClient;

    public async ValueTask<ApiResponse<IEnumerable<AvailableModelsResponse>>> BrowseAvailableModels() 
        => await _httpClient.GetAsync<IEnumerable<AvailableModelsResponse>>($"/models");
}
