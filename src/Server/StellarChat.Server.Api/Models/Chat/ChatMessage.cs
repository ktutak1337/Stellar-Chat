namespace StellarChat.Server.Api.Models.Chat;

public class ChatMessage
{
    /// <summary>
    /// Id of the message.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id of the chat this message belongs to.
    /// </summary>
    public Guid ChatId { get; set; }
    public ChatMessageType Type { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
    public int TokenCount { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public ChatMessage(Guid id, Guid chatId, ChatMessageType type, string author, string content, int tokenCount, DateTimeOffset timestamp)
    {
        Id = id;
        ChatId = chatId;
        Type = type;
        Author = author;
        Content = content;
        TokenCount = tokenCount;
        Timestamp = timestamp;
    }

    public static ChatMessage Create(Guid id, Guid chatId, ChatMessageType type, string author, string content, int tokenCount, DateTimeOffset timestamp)
        => new(id, chatId, type, author, content, tokenCount, timestamp);
}
