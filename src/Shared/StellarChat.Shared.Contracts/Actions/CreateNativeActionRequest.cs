using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Actions;

public record CreateNativeActionRequest(
    [property: JsonIgnore] Guid Id,
    string Name,
    string Category,
    string Icon,
    string Model,
    string Metaprompt,
    bool IsRemoteAction,
    bool ShouldRephraseResponse,
    Webhook? Webhook);
