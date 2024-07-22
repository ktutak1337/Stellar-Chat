using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Models;

namespace StellarChat.Client.Web.Services.Models;

public class ModelCatalogService(IRestHttpClient httpClient) : IModelCatalogService
{
    private readonly IRestHttpClient _httpClient = httpClient;

    public async ValueTask<ApiResponse<IEnumerable<ModelCatalogResponse>>> BrowseModelsCatalog() 
        => await _httpClient.GetAsync<IEnumerable<ModelCatalogResponse>>($"/models?Provider=openai&Filter=completions");
}
