namespace StellarChat.Shared.Abstractions.Exceptions;

public abstract class StellarChatException : Exception
{
    public string UserMessage { get; } = string.Empty;

    protected StellarChatException(string message, string userMessage)
        : base(message) 
    { 
        UserMessage = userMessage; 
    }
}
