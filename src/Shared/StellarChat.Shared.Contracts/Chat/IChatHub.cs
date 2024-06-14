using StellarChat.Shared.Contracts.Actions;

namespace StellarChat.Shared.Contracts.Chat;

public interface IChatHub
{
    Task ReceiveChatMessageChunk(string message);
    Task ReceiveProcessingStatus(string message, RemoteActionStatus status);
}
