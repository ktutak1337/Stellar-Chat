namespace StellarChat.Server.Api.Features.Chat.CarryConversation.Services;

internal interface IChatContext
{
    Task UpdateMessageOnClient(ChatMessage chatMessage, string message, CancellationToken cancellationToken = default);
}
