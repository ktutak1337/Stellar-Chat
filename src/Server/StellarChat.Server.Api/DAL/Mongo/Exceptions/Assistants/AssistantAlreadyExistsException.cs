namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Assistants;

public class AssistantAlreadyExistsException : StellarChatException
{
    public Guid Id { get; }

    public AssistantAlreadyExistsException(Guid id)
        : base(
            message: $"Assistant with Id: '{id}' already exists.",
            userMessage: $"An assistant with the same details already exists.")
        => Id = id;
}
