namespace StellarChat.Shared.Contracts.Chat;

public interface IChatHub
{
    Task ReceiveChatMessageChunk(string message);
}
