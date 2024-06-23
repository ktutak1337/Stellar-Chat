namespace StellarChat.Server.Api.Features.Chat.SearchChatSessions;

internal sealed class SearchChatSessions : PagedQuery<ChatSessionResponse>
{
    public string Query { get; set; } = string.Empty;
}
