using Microsoft.SemanticKernel;
using StellarChat.Server.Api.Features.Actions.CreateNativeAction;
using StellarChat.Server.Api.Features.Actions.Webhooks.Services;
using StellarChat.Server.Api.Features.Chat.CarryConversation;

namespace StellarChat.Server.Api.Features.Actions.ExecuteNativeAction;

internal sealed class ExecuteNativeActionHandler : ICommandHandler<ExecuteNativeAction, string>
{
    private readonly INativeActionRepository _nativeActionRepository;
    private readonly IHttpClientService _httpClientService;
    private readonly IChatContext _chatContext;
    private readonly Kernel _kernel;
    private readonly IHubContext<ChatHub, IChatHub> _hubContext;
    private readonly TimeProvider _clock;
    private readonly ILogger<CreateNativeActionHandler> _logger;

    public ExecuteNativeActionHandler(
        INativeActionRepository nativeActionRepository,
        IHttpClientService httpClientService,
        IChatContext chatContext,
        Kernel kernel,
        IHubContext<ChatHub, IChatHub> hubContext,
        TimeProvider clock,
        ILogger<CreateNativeActionHandler> logger)
    {
        _nativeActionRepository = nativeActionRepository;
        _httpClientService = httpClientService;
        _chatContext = chatContext;
        _kernel = kernel;
        _hubContext = hubContext;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<string> Handle(ExecuteNativeAction command, CancellationToken cancellationToken)
    {
        var (id, chatId, message) = command;
        
        var action = await _nativeActionRepository.GetAsync(id) ?? throw new NativeActionNotFoundException(id);

        action.Metaprompt = action.Metaprompt.IsEmpty() 
            ? string.Empty 
            : _clock.ReplaceDatePlaceholder(action.Metaprompt);
        
        string semanticResponse = string.Empty;

        await _chatContext.SetChatInstructions(chatId, action.Metaprompt);
        await _chatContext.ExtractChatHistoryAsync(chatId);

        var userMessage = CreateUserMessage(chatId, message);
        await _chatContext.SaveChatMessageAsync(chatId, userMessage);

        var botMessage = CreateBotMessage(chatId, content: string.Empty);
        var botResponseMessage = await _chatContext.StreamResponseToClientAsync(chatId, action.Model, botMessage, _hubContext);

        semanticResponse = botResponseMessage.Content;

        if (action.IsRemoteAction)
        {
            var response = await _httpClientService.PostAsync(action.Webhook!.Url, semanticResponse, action.Webhook.Headers, cancellationToken);
            botResponseMessage.Content = response;
            await _hubContext.Clients.All.ReceiveChatMessageChunk(response);
            await _chatContext.SaveChatMessageAsync(chatId, botResponseMessage);

            return response;
        }

        await _chatContext.SaveChatMessageAsync(chatId, botResponseMessage);

        return semanticResponse;
    }

    private ChatMessage CreateUserMessage(Guid chatId, string message, ChatMessageType messageType = ChatMessageType.Message)
    {
        var now = _clock.GetLocalNow();

        var userMessage = ChatMessage.Create(Guid.NewGuid(), chatId, messageType, Author.User, message, tokenCount: 0, now);
        return userMessage;
    }

    private ChatMessage CreateBotMessage(Guid chatId, string content, ChatMessageType messageType = ChatMessageType.Message)
    {
        var now = _clock.GetLocalNow();

        var chatMessage = ChatMessage.Create(Guid.NewGuid(), chatId, messageType, Author.Bot, content, tokenCount: 0, now);
        return chatMessage;
    }
}
