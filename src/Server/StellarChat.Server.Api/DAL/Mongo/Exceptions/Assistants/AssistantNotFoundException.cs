namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Assistants;

public class AssistantNotFoundException : StellarChatException
{
    public Guid Id { get; }

    public AssistantNotFoundException(Guid id)
        : base(
            message: $"Assistant message with ID: {id} not found.",
            userMessage: $"The requested assistant could not be found.")
        => Id = id;
}

