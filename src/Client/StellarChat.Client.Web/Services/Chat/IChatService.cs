using StellarChat.Client.Web.Shared;
using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Chat;

namespace StellarChat.Client.Web.Services.Chat;

public interface IChatService
{
    ValueTask<ApiResponse<Paged<ChatSessionResponse>>> BrowseChatSessions(int page = 1, int pageSize = 0);
    ValueTask<ApiResponse<Paged<ChatSessionResponse>>> SearchChatSessions(string query, int page = 1, int pageSize = 0);
    ValueTask<ApiResponse<ChatSessionResponse>> GetChatSession(Guid id);
    ValueTask<ApiResponse<Paged<ChatMessageResponse>>> GetChatMessagesByChatId(Guid chatId, int page = 1, int pageSize = 0);
    ValueTask<ApiResponse> SendMessage(Guid chatId, string message, string messageType, string model);
    ValueTask<ApiResponse> ChangeChatSessionTitle(Guid id, string title);
    ValueTask<ApiResponse<Guid>> CreateChatSession(Guid assistantId, string title, string message);
    ValueTask<ApiResponse> DeleteChatSession(Guid id);
}
