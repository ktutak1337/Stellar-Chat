namespace StellarChat.Shared.Abstractions.Contracts.Chat;

public record ChatMessageResponse(Guid Id, Guid ChatId, ChatMessageType Type, string Author, string Content, DateTimeOffset Timestamp);
