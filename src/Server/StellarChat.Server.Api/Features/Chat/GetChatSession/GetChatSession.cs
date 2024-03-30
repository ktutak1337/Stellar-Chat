using Mediator;
using StellarChat.Shared.Abstractions.Contracts.Chat;

namespace StellarChat.Server.Api.Features.Chat.GetChatSession;

internal sealed record GetChatSession : IQuery<ChatSessionResponse>
{
    public Guid Id { get; set; }
}
