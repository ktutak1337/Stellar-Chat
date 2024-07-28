using Microsoft.SemanticKernel;
using StellarChat.Server.Api.Features.Chat.CarryConversation;
using StellarChat.Server.Api.Options;

namespace StellarChat.Server.Api.Features.Models.Connectors.Providers;

internal sealed class OpenAiProvider : IConnector
{
    private readonly OpenAiOptions _options;
    private readonly IChatContext _chatContext;
    private readonly TimeProvider _clock;
    public Kernel Kernel { get; private set; } = null!;
    public string ProviderName => "OpenAI";

    public OpenAiProvider(OpenAiOptions options, IChatContext chatContext, TimeProvider clock)
    {
        _options = options;
        _chatContext = chatContext;
        _clock = clock;
    }

    public Kernel CreateKernel(string modelId)
    {
        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: modelId,
                apiKey: _options.API_KEY)
            .Build();

        kernel.ImportPluginFromObject(new ChatPlugin(_chatContext, _clock), nameof(ChatPlugin));

        Kernel = kernel;
        return kernel;
    }
}
