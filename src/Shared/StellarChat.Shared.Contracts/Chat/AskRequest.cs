using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Chat;

public record AskRequest([property: JsonIgnore] Guid ChatId, Guid? AssistantId, string Message, string MessageType, string Model);
