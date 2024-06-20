using Webhook = StellarChat.Server.Api.Domain.Actions.Models.Webhook;

namespace StellarChat.Server.Api.Features.Actions.UpdateNativeAction;

internal sealed record UpdateNativeAction(
    [Required] Guid Id,
    string Name,
    string Category,
    string Icon,
    string Model,
    string Metaprompt,
    bool IsSingleMessageMode,
    bool IsRemoteAction,
    bool ShouldRephraseResponse,
    Webhook? Webhook) : ICommand;
