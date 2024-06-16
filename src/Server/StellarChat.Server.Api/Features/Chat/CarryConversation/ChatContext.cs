using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;

namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal class ChatContext : IChatContext
{
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IAssistantRepository _assistantRepository;
    private readonly Kernel _kernel;
    private readonly ChatHistory _chatHistory = new();
    private readonly TimeProvider _clock;

    public ChatContext(
        IChatSessionRepository chatSessionRepository,
        IChatMessageRepository chatMessageRepository,
        IAssistantRepository assistantRepository,
        Kernel kernel,
        TimeProvider clock)
    {
        _chatSessionRepository = chatSessionRepository;
        _chatMessageRepository = chatMessageRepository;
        _assistantRepository = assistantRepository;
        _kernel = kernel;
        _clock = clock;
    }

    public async Task SetChatInstructions(Guid chatId, string? actionMetaprompt = null)
    {
        if(actionMetaprompt!.IsNotEmpty())
        {
            _chatHistory.AddSystemMessage(actionMetaprompt!);
            return;
        }

        var chatSession = await _chatSessionRepository.GetAsync(chatId) ?? throw new ChatSessionNotFoundException(chatId);
        var assistants = await _assistantRepository.BrowseAsync();
        var defaultAssistant = assistants.SingleOrDefault(assistant => assistant.IsDefault);
        var isDefaultAssistant = chatSession.AssistantId != Guid.Empty && chatSession.AssistantId == defaultAssistant?.Id;
        
        var assistant = isDefaultAssistant
            ? defaultAssistant
            : assistants.SingleOrDefault(assistant => assistant.Id == chatSession.AssistantId) ?? defaultAssistant;

        assistant!.Metaprompt = assistant.Metaprompt.IsEmpty()
            ? string.Empty
            : _clock.ReplaceDatePlaceholder(assistant.Metaprompt);

        _chatHistory.AddSystemMessage(assistant!.Metaprompt);
    }

    public async Task ExtractChatHistoryAsync(Guid chatId)
    {
        var messages = await _chatMessageRepository.FindMessagesByChatIdAsync(chatId);

        var filteredAndSortedMessages = messages
            .Where(chatMessage => chatMessage.Type != ChatMessageType.File)
            .OrderByDescending(chatMessage => chatMessage.Timestamp);

        foreach (var message in filteredAndSortedMessages)
        {
            if (message.Author == Author.Bot)
            {
                _chatHistory.AddAssistantMessage(message.Content.Trim());
            }
            else
            {
                _chatHistory.AddUserMessage(message.Content.Trim());
            }
        }

        _chatHistory.Reverse();
    }

    public async Task<ChatMessage> StreamResponseToClientAsync(
        Guid chatId, string model, ChatMessage botMessage, bool isRemoteAction, IHubContext<ChatHub, IChatHub> hubContext, CancellationToken cancellationToken = default)
    {
        var reply = new StringBuilder();
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        var executionSettings = new PromptExecutionSettings
        {
            ModelId = model
        };

        await foreach (var contentPiece in chatCompletionService.GetStreamingChatMessageContentsAsync(_chatHistory, executionSettings))
        {
            if (contentPiece.Content is { Length: > 0 })
            {
                reply.Append(contentPiece.Content);
                botMessage.Content = reply.ToString();

                if(!isRemoteAction)
                {
                    await hubContext.Clients.All.ReceiveChatMessageChunk(contentPiece.Content);
                }
            }
        }

        reply.Clear();
        return botMessage;
    }

    public async Task SaveChatMessageAsync(Guid chatId, ChatMessage message)
    {
        await EnsureChatSessionExistsAsync(chatId);

        if (message.Author == Author.User)
        {
            await _chatMessageRepository.AddAsync(message);
            _chatHistory.AddUserMessage(message.Content);
        }
        else
        {
            await _chatMessageRepository.AddAsync(message);

            _chatHistory.AddAssistantMessage(message.Content);
        }
    }

    private async Task EnsureChatSessionExistsAsync(Guid chatId)
    {
        if (!await _chatSessionRepository.ExistsAsync(chatId))
        {
            throw new ChatSessionNotFoundException(chatId);
        }
    }
}
