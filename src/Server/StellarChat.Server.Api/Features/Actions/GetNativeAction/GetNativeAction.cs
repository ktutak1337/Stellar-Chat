namespace StellarChat.Server.Api.Features.Actions.GetNativeAction;

internal sealed record GetNativeAction : IQuery<NativeActionResponse>
{
    public Guid Id { get; set; }
}
