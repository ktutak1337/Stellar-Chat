namespace StellarChat.Server.Api.Features.Actions.Webhooks.Exceptions;

public class RemoteActionExecutionFailedException : StellarChatException
{
    public Guid Id { get; }
    public string Name { get; }

    public RemoteActionExecutionFailedException(Guid id, string name)
        : base(
            message: $"Remote action with Id: '{id}' and Name: '{name}' failed to execute.",
            userMessage: $"The action '{name}' could not be completed. Please try again later. Check logs for more details.")
        => (Id, Name) = (id, name);
}
