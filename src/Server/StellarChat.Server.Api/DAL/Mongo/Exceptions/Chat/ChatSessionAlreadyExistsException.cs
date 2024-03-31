using StellarChat.Shared.Abstractions.Exceptions;

namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Chat;

public class ChatSessionAlreadyExistsException : StellarChatException
{
    public Guid Id { get; }

    public ChatSessionAlreadyExistsException(Guid id)
        : base(
            message: $"Chat session with Id: '{id}' already exists.",
            userMessage: $"A chat session with the same details already exists.")
        => Id = id;
}
