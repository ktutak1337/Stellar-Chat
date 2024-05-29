namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Actions;

public class NativeActionAlreadyExistsException : StellarChatException
{
    public Guid Id { get; }

    public NativeActionAlreadyExistsException(Guid id)
        : base(
            message: $"Action with Id: '{id}' already exists.",
            userMessage: $"A similar action already exists.")
        => Id = id;
}
