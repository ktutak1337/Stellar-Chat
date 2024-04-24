using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Chat;

public record ChangeChatSessionTitleRequest([property: JsonIgnore] Guid ChatId, string Title);
