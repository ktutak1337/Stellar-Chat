using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal class ChatPlugin
{
    private readonly IChatContext _chatContext;
    private readonly TimeProvider _clock;

    public ChatPlugin(IChatContext chatContext, TimeProvider clock)
    {
        _chatContext = chatContext;
        _clock = clock;
    }

    [KernelFunction, Description("Get chat response")]
    public async Task<KernelArguments> ChatAsync(
        [Description("The new message used as input")] string message,
        [Description("Unique identifier for the chat")] Guid chatId,
        [Description("Model used for text generation (e.g., gpt-4, gpt-3.5-turbo)")] string model,
        IHubContext<ChatHub, IChatHub> hubContext,
        KernelArguments context)
    {
        await _chatContext.SetChatInstructions(chatId);
        await _chatContext.ExtractChatHistoryAsync(chatId);
        
        var userMessage = CreateUserMessage(chatId, message);
        await _chatContext.SaveChatMessageAsync(chatId, userMessage);

        var botMessage = CreateBotMessage(chatId, content: string.Empty);
        var botResponseMessage = await _chatContext.StreamResponseToClientAsync(chatId, model, botMessage, hubContext);
        await _chatContext.SaveChatMessageAsync(chatId, botResponseMessage);

        context["input"] = botResponseMessage.Content;

        return context;
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
