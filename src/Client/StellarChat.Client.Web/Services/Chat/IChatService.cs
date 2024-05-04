using MudBlazor;
using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Chat;

namespace StellarChat.Client.Web.Services.Chat;

public interface IChatService
{
    ValueTask<Paged<ChatSessionResponse>> BrowseChatSessions(int page = 0, int pageSize = 0);
}
