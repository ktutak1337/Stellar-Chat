namespace StellarChat.Server.Api.Features.Chat.GetChatSession;

internal sealed record GetChatSession : IQuery<ChatSessionResponse>
{
    public Guid Id { get; set; }
}
