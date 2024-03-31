using System.Text.Json.Serialization;

namespace StellarChat.Shared.Abstractions.Contracts.Chat;

public record CreateChatSessionRequest([property: JsonIgnore] Guid? ChatId, string Title);
