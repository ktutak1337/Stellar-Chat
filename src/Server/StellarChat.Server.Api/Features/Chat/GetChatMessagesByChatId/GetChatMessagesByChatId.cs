using StellarChat.Shared.Abstractions.Contracts.Chat;
using StellarChat.Shared.Abstractions.Pagination;

namespace StellarChat.Server.Api.Features.Chat.GetChatMessagesByChatId;

internal sealed class GetChatMessagesByChatId : PagedQuery<ChatMessageResponse>
{
    public Guid ChatId { get; set; }
}
