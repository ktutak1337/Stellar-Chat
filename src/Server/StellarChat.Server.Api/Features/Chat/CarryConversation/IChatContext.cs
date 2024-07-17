using Microsoft.SemanticKernel;

namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal interface IChatContext
{
    Task SetChatInstructions(Guid chatId, string? actionMetaprompt = null);
    Task ExtractChatHistoryAsync(Guid chatId);
    Task SaveChatMessageAsync(Guid chatId, ChatMessage message);
    Task<ChatMessage> StreamResponseToClientAsync(Guid chatId, string model, string serviceId, ChatMessage botMessage, bool isRemoteAction, IHubContext<ChatHub, IChatHub> hubContext, Kernel kernel, CancellationToken cancellationToken = default);
}
