namespace StellarChat.Server.Api.DAL.Mongo.Exceptions.Chat;

internal sealed class ChatSessionNotFoundException : StellarChatException
{
    public Guid Id { get; }
    public string? Title { get; }

    public ChatSessionNotFoundException(Guid id)
        : base(
            message: $"Chat session with ID: {id} not found.",
            userMessage: $"The requested chat session could not be found.")
        => Id = id;

    public ChatSessionNotFoundException(string title)
        : base(
            message: $"Chat session with title: {title} not found.",
            userMessage: $"The chat session with the title '{title}' could not be found.")
        => Title = title;
}
