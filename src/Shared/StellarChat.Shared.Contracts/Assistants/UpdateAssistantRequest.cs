using System.ComponentModel.DataAnnotations;

namespace StellarChat.Shared.Contracts.Assistants;

public record UpdateAssistantRequest(
    [Required] Guid Id,
    string Name,
    string Metaprompt,
    string Description,
    string AvatarUrl,
    string DefaultModel,
    string DefaultVoice,
    bool IsDefault);
