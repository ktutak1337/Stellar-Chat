namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal interface IChatContext
{
    Task SetChatInstructions(Guid chatId);
    Task ExtractChatHistoryAsync(Guid chatId);
    Task SaveChatMessageAsync(Guid chatId, ChatMessage message);
    Task<ChatMessage> StreamResponseToClientAsync(Guid chatId, string model, ChatMessage botMessage, IHubContext<ChatHub, IChatHub> hubContext, CancellationToken cancellationToken = default);
}
