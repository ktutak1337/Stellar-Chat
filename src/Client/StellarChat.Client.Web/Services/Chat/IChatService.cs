using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Chat;

namespace StellarChat.Client.Web.Services.Chat;

public interface IChatService
{
    ValueTask<Paged<ChatSessionResponse>> BrowseChatSessions(int page = 0, int pageSize = 10000);
    ValueTask<ChatSessionResponse> GetChatSession(Guid id);
    ValueTask<Paged<ChatMessageResponse>> GetChatMessagesByChatId(Guid chatId, int page = 0, int pageSize = 10000);
    ValueTask SendMessage(Guid chatId, string message, string messageType, string model);
    ValueTask ChangeChatSessionTitle(Guid id, string title);
    ValueTask<Guid> CreateChatSession(Guid assistantId, string title, string message);
    ValueTask<HttpResponseMessage> DeleteChatSession(Guid id);
}
