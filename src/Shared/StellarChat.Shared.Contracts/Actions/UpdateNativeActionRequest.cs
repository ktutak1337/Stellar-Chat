using System.ComponentModel.DataAnnotations;

namespace StellarChat.Shared.Contracts.Actions;

public sealed record UpdateNativeActionRequest(
    [Required] Guid Id,
    string Name,
    string Category,
    string Icon,
    string Model,
    string Metaprompt,
    bool IsRemoteAction,
    bool ShouldRephraseResponse,
    Webhook? Webhook);
