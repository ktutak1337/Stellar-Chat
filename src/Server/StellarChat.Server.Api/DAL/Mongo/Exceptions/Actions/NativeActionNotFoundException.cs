namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Actions;

public class NativeActionNotFoundException : StellarChatException
{
    public Guid Id { get; }

    public NativeActionNotFoundException(Guid id)
        : base(
            message: $"Action with ID: {id} not found.",
            userMessage: $"The requested action could not be found.")
        => Id = id;
}
