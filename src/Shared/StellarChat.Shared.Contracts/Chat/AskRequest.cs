using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Chat;

public record AskRequest([property: JsonIgnore] Guid ChatId, string Message, string MessageType, string Model);
