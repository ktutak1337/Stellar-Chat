namespace StellarChat.Server.Api.Domain.Chat.Repositories;

internal interface IChatMessageRepository
{
    /// <summary>
    /// Retrieves a specific chat message by message ID.
    /// </summary>
    /// <param name="messageId">The message ID.</param>
    /// <returns>A ChatMessage with the specified chatId, otherwise null if no such message exists.</returns>
    ValueTask<ChatMessage?> GetAsync(Guid messageId);

    /// <summary>
    /// Retrieves all chat messages from all chat sessions.
    /// </summary>
    /// <returns>A collection of all ChatMessages, regardless of the chat they belong to.</returns>
    ValueTask<IEnumerable<ChatMessage>> BrowseMessagesAsync();

    /// <summary>
    /// Finds chat messages by chat ID.
    /// </summary>
    /// <param name="chatId">The chat ID.</param>
    /// <returns>A list of ChatMessages matching the given chatId.</returns>
    ValueTask<IEnumerable<ChatMessage>> FindMessagesByChatIdAsync(Guid chatId);

    /// <summary>
    /// Finds the most recent chat message by chat ID.
    /// </summary>
    /// <param name="chatId">The chat id.</param>
    /// <returns>The most recent ChatMessage matching the given chatId.</returns>
    ValueTask<ChatMessage?> FindLatestMessageByChatIdAsync(Guid chatId);

    /// <summary>
    /// Adds a new chat message to the chat message repository.
    /// </summary>
    /// <param name="message">The chat message to add.</param>
    ValueTask AddAsync(ChatMessage message);

    /// <summary>
    /// Updates an existing chat message in the chat message repository.
    /// </summary>
    /// <param name="message">The chat message to update.</param>
    ValueTask UpdateAsync(ChatMessage message);

    /// <summary>
    /// Deletes a chat message without regard to its association with any chat session by message ID
    /// </summary>
    /// <param name="messageId">The message ID.</param>
    ValueTask DeleteAsync(Guid messageId);

    /// <summary>
    /// Checks if a chat message with the specified unique message ID exists, regardless of the chat they belong to.
    /// </summary>
    /// <param name="messageId">The message ID.</param>
    /// <returns> Retruns Bolean:
    /// true if a chat message with the specified identifier exists; otherwise flase</returns>
    ValueTask<bool> ExistsAsync(Guid messageId);
}
