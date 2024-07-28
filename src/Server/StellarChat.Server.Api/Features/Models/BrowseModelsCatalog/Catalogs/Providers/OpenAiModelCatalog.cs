using Newtonsoft.Json;
using StellarChat.Server.Api.Features.Actions.Webhooks.Services;
using StellarChat.Server.Api.Options;

namespace StellarChat.Server.Api.Features.Models.BrowseModelsCatalog.Catalogs.Providers;

internal class OpenAiModelCatalog(IHttpClientService httpClientService, OpenAiOptions openAiOptions) : IModelCatalog
{
    public string ProviderName => OpenAIVendor;
    private const string OpenAIApiEndpoint = "https://api.openai.com/v1/models";
    private const string OpenAIVendor = "Openai";
    
    private readonly IHttpClientService _httpClientService = httpClientService;
    private readonly OpenAiOptions _openAiOptions = openAiOptions;

    public async ValueTask<IEnumerable<ModelCatalog>> FetchModelsAsync(BrowseModelsCatalog query, CancellationToken cancellationToken = default)
    {
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {_openAiOptions.API_KEY}" }
        };

        var response = await _httpClientService.GetAsync(OpenAIApiEndpoint, headers, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken) ?? string.Empty;
        var responseData = JsonConvert.DeserializeObject<OpenAiModelResponse>(content);

        if (responseData?.Data is null)
        {
            return [];
        }

        return responseData.Data.Select(model => new ModelCatalog
        {
            Name = model.Id,
            Vendor = ProviderName,
            Provider = ProviderName,
            CreatedAt = DateTimeOffset.FromUnixTimeSeconds(model.Created)
        }).ToList();
    }

    public IEnumerable<ModelCatalog> FilterModels(string filter, IEnumerable<ModelCatalog> models)
    {
        return filter switch
        {
            "audio" => models.Where(model => model.Name.Contains("tts") || model.Name.Contains("whisper")),
            "completions" => models.Where(model => model.Name.Contains("gpt")),
            "embeddings" => models.Where(model => model.Name.Contains("embedding")),
            "images" => models.Where(model => model.Name.Contains("dall-e") || model.Name.Contains("vision")),
            _ => models
        };
    }

    record OpenAiModelResponse(string Object, List<OpenAiModel> Data);
    record OpenAiModel(string Id, string Object, long Created, string OwnedBy);
}
