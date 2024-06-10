using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Chat;

public record CreateChatSessionRequest([property: JsonIgnore] Guid? ChatId, Guid AssistantId, string Title);
