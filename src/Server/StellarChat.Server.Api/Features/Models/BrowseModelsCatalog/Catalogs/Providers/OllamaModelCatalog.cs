using Newtonsoft.Json;
using StellarChat.Server.Api.Features.Actions.Webhooks.Services;

namespace StellarChat.Server.Api.Features.Models.BrowseModelsCatalog.Catalogs.Providers;

internal class OllamaModelCatalog(IHttpClientService httpClientService, ISettingsRepository settingsRepository) : IModelCatalog
{
    public string ProviderName => OllamaVendor;
    private const string OllamaVendor = "Ollama";

    private readonly IHttpClientService _httpClientService = httpClientService;
    private readonly ISettingsRepository _settingsRepository = settingsRepository;

    public IEnumerable<ModelCatalog> FilterModels(string filter, IEnumerable<ModelCatalog> models)
    {
        return [];
    }

    public async ValueTask<IEnumerable<ModelCatalog>> FetchModelsAsync(BrowseModelsCatalog query, CancellationToken cancellationToken)
    {
        var settings = await _settingsRepository.GetSettingsByKeyAsync("app-settings");

        var ollamaSettings = settings.Integrations.SingleOrDefault(x => x.Name.Equals(ProviderName, StringComparison.InvariantCultureIgnoreCase));

        if (ollamaSettings!.Endpoint.IsEmpty() && ollamaSettings.IsEnabled)
        {
            return [];
        }

        var ollamaApiEndpoint = $"{ollamaSettings.Endpoint}/api/tags";

        var response = await _httpClientService.GetAsync(ollamaApiEndpoint, headers: null, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken) ?? string.Empty;
        var responseData = JsonConvert.DeserializeObject<OllamaModelResponse>(content);

        if (responseData?.Models is null)
        {
            return [];
        }

        return responseData.Models.Select(model => new ModelCatalog
        {
            Name = model.Name,
            Vendor = ProviderName,
            Provider = ProviderName,
            CreatedAt = model.ModifiedAt
        }).ToList();
    }

    record OllamaModelResponse(List<OllamaModel> Models);
    record OllamaModel(string Name, [property: JsonProperty("modified_at")] DateTimeOffset ModifiedAt, long Size, string Digest, OllamaModelDetails Details);
    record OllamaModelDetails(string Format, string Family, List<string>? Families, string ParameterSize, string QuantizationLevel);
}
