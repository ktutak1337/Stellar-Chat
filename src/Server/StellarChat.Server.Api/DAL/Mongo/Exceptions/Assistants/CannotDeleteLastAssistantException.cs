namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Assistants;

public class CannotDeleteLastAssistantException : StellarChatException
{
    public Guid Id { get; }

    public CannotDeleteLastAssistantException(Guid id)
        : base(
            message: $"Cannot delete the assistant with ID: {id}. At least one assistant must always be present in the system.",
            userMessage: $"The assistant cannot be deleted because the system requires at least one assistant to function properly.")
        => Id = id;
}
