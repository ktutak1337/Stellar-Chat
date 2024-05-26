namespace StellarChat.Shared.Contracts.Chat;

public class ChatMessageResponse
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public ChatMessageType Type { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset Timestamp { get; set; }
}
