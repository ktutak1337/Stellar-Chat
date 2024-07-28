using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Models;

namespace StellarChat.Client.Web.Services.Models;

public class ModelCatalogService(IRestHttpClient httpClient) : IModelCatalogService
{
    private readonly IRestHttpClient _httpClient = httpClient;

    public async ValueTask<ApiResponse<ModelCatalogResponse>> BrowseModelsCatalog(string provider = "openai", string filter = "completions", bool forceRefresh = false) 
        => await _httpClient.GetAsync<ModelCatalogResponse>($"/models?Provider={provider}&Filter={filter}&forceRefresh={forceRefresh}");
}
