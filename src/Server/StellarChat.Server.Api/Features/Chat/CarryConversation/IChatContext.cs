namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal interface IChatContext
{
    Task SetChatInstructions(Guid chatId, string? actionMetaprompt = null);
    Task ExtractChatHistoryAsync(Guid chatId);
    Task SaveChatMessageAsync(Guid chatId, ChatMessage message);
    Task<ChatMessage> StreamResponseToClientAsync(Guid chatId, string model, ChatMessage botMessage, bool isRemoteAction, IHubContext<ChatHub, IChatHub> hubContext, CancellationToken cancellationToken = default);
}
