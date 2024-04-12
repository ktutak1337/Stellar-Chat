namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Chat;

public class ChatMessageAlreadyExistsException : StellarChatException
{
    public Guid Id { get; }

    public ChatMessageAlreadyExistsException(Guid id)
        : base(
            message: $"Chat message with Id: '{id}' already exists.",
            userMessage: $"A chat message with the same details already exists.")
        => Id = id;
}
