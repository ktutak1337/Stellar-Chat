using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Assistants;

public record CreateAssistantRequest(
    [property: JsonIgnore] Guid Id,
    string Name,
    string Metaprompt,
    string Description,
    string AvatarUrl,
    string DefaultModel,
    string DefaultVoice,
    bool IsDefault);