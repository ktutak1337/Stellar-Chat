namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Chat;

public class ChatMessageNotFoundException : StellarChatException
{
    public Guid Id { get; }

    public ChatMessageNotFoundException(Guid id)
        : base(
            message: $"Chat message with ID: {id} not found.",
            userMessage: $"The requested chat message could not be found.")
        => Id = id;
}
