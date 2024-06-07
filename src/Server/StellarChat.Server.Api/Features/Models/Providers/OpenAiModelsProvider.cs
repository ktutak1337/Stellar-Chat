using Newtonsoft.Json;
using StellarChat.Server.Api.Features.Actions.Webhooks.Services;
using StellarChat.Server.Api.Options;

namespace StellarChat.Server.Api.Features.Models.Providers;

internal sealed class OpenAiModelsProvider : IModelsProvider
{
    private const string OpenAIApiEndpoint = "https://api.openai.com/v1/models";
    private const string OpenAIVendor = "OpenAI";

    private readonly IHttpClientService _httpClientService;
    private readonly OpenAiOptions _openAiOptions;

    public OpenAiModelsProvider(IHttpClientService httpClientService, OpenAiOptions openAiOptions)
    {
        _httpClientService = httpClientService;
        _openAiOptions = openAiOptions;
    }

    public async ValueTask<IEnumerable<AvailableModelsResponse>> FetchModelsAsync(CancellationToken cancellationToken = default)
    {
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {_openAiOptions.API_KEY}" }
        };

        var response = await _httpClientService.GetAsync(OpenAIApiEndpoint, headers, cancellationToken);
        var responseData = JsonConvert.DeserializeObject<OpenAiModelResponse>(response);

        if (responseData?.Data is null)
        {
            return [];
        }

        return responseData.Data.Select(model => new AvailableModelsResponse
        {
            Name = model.Id,
            Vendor = OpenAIVendor,
            Provider = OpenAIVendor,
            CreatedAt = DateTimeOffset.FromUnixTimeSeconds(model.Created)
        }).ToList();
    }

    record OpenAiModelResponse(string Object, List<OpenAiModel> Data);
    record OpenAiModel(string Id, string Object, long Created, string OwnedBy);
}
