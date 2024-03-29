﻿using StellarChat.Server.Api.Domain.Chat.Models;
using StellarChat.Shared.Infrastructure.DAL.Mongo;

namespace StellarChat.Server.Api.DAL.Mongo.Documents.Chat;

internal class ChatMessageDocument : IIdentifiable<Guid>
{
    /// <summary>
    /// Id of the message.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id of the chat this message belongs to.
    /// </summary>
    public Guid ChatId { get; set; }
    public ChatMessageType Type { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int TokenCount { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}
