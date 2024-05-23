using Microsoft.SemanticKernel;

namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal sealed class CarryConversationHandler : ICommandHandler<Ask, string>
{
    private const string ChatPluginName = nameof(ChatPlugin);
    private const string ChatFunctionName = "Chat";

    private readonly Kernel _kernel;
    private readonly IHubContext<ChatHub, IChatHub> _hubContext;

    public CarryConversationHandler(Kernel kernel, IHubContext<ChatHub, IChatHub> hubContext)
    {
        _kernel = kernel;
        _hubContext = hubContext;
    }

    public async ValueTask<string> Handle(Ask command, CancellationToken cancellationToken)
    {
        var contextVariables = GetContextArguments(command);

        KernelFunction? chatFunction = _kernel.Plugins.GetFunction(ChatPluginName, ChatFunctionName);
        await _kernel.InvokeAsync(chatFunction!, contextVariables, cancellationToken);

        contextVariables.TryGetValue("input", out var result);
        
        return result!.ToString() ?? string.Empty;
    }

    private KernelArguments GetContextArguments(Ask command)
    {
        var contextArguments = new KernelArguments();

        contextArguments.TryAdd("message", command.Message);
        contextArguments.TryAdd("messageType", command.MessageType);
        contextArguments.TryAdd("model", command.Model);
        contextArguments.TryAdd("chatId", command.ChatId);
        contextArguments.TryAdd("hubContext", _hubContext);

        return contextArguments;
    }
}
