using Webhook = StellarChat.Server.Api.Domain.Actions.Models.Webhook;

namespace StellarChat.Server.Api.Features.Actions.CreateNativeAction;

internal sealed record CreateNativeAction(
    [Required] Guid Id,
    string Name,
    string Category,
    string Icon,
    string Model,
    string Metaprompt,
    bool IsRemoteAction,
    bool ShouldRephraseResponse,
    Webhook? Webhook) : ICommand;
