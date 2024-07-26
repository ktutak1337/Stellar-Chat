using Microsoft.SemanticKernel;
using StellarChat.Server.Api.Features.Chat.CarryConversation;
using StellarChat.Server.Api.Options;

namespace StellarChat.Server.Api.Features.Models.Connectors.Providers;

internal sealed class OllamaProvider : IConnector
{
    private readonly OllamaOptions _options;
    private readonly ISettingsRepository _settingsRepository;
    private readonly IChatContext _chatContext;
    private readonly TimeProvider _clock;
    private readonly string _endpoint;

    public Kernel Kernel { get; private set; } = null!;
    public string ProviderName => "ollama";

    public OllamaProvider(OllamaOptions options, ISettingsRepository settingsRepository, IChatContext chatContext, TimeProvider clock)
    {
        _options = options;
        _settingsRepository = settingsRepository;
        _chatContext = chatContext;
        _clock = clock;
        _endpoint = GetEndpoint().GetAwaiter().GetResult();
    }

    public Kernel CreateKernel(string modelId)
    {
        if (_options.ENABLED)
        {
            #pragma warning disable SKEXP0010
            Kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: modelId,
                apiKey: null,
                endpoint: new Uri(_endpoint))
            .Build();
            #pragma warning restore SKEXP0010

            Kernel.ImportPluginFromObject(new ChatPlugin(_chatContext, _clock), nameof(ChatPlugin));
        }

        return Kernel;
    }

    private async Task<string> GetEndpoint()
    {
        if(!_options.ENABLED)
        {
            return string.Empty;
        }

        var envEndpoint = Environment.GetEnvironmentVariable("OLLAMA_ENDPOINT");

        if (envEndpoint!.IsNotEmpty())
        {
            return envEndpoint!;
        }

        var settings = await _settingsRepository.GetSettingsByKeyAsync("app-settings");
        var ollamaSettings = settings.Integrations.SingleOrDefault(x => x.Name.Equals(OllamaOptions.Key, StringComparison.InvariantCultureIgnoreCase));
        
        if (ollamaSettings is not null && ollamaSettings.Endpoint.IsNotEmpty())
        {
            return ollamaSettings.Endpoint;
        }

        throw new InvalidOperationException("Ollama endpoint configuration is missing.");
    }
}
