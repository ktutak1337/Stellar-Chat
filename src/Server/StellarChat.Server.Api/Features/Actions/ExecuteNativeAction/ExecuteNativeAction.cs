namespace StellarChat.Server.Api.Features.Actions.ExecuteNativeAction;

internal sealed record ExecuteNativeAction([Required] Guid Id, [Required] Guid ChatId, string ServiceId, string Message) : ICommand<string>;
