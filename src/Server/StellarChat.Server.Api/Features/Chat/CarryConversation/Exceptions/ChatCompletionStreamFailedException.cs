namespace StellarChat.Server.Api.Features.Chat.CarryConversation.Exceptions;

public class ChatCompletionStreamFailedException : StellarChatException
{
    public Guid ChatId { get; }

    public ChatCompletionStreamFailedException(Guid chatId, string reason)
        : base(
            message: $"Failed to stream chat completion for chat session with ID: {chatId}. Reason: {reason}",
            userMessage: $"Unable to stream chat completion for the chat session with ID '{chatId}'. Reason: {reason}") 
        => ChatId = chatId;
}
