using StellarChat.Client.Web.Shared;
using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Chat;

namespace StellarChat.Client.Web.Services.Chat;

public class ChatService(IRestHttpClient httpClient) : IChatService
{
    private readonly IRestHttpClient _httpClient = httpClient;

    public async ValueTask<ApiResponse<Paged<ChatSessionResponse>>> BrowseChatSessions(int page = 1, int pageSize = 0) 
        => await _httpClient.GetAsync<Paged<ChatSessionResponse>>($"/chat-history/sessions?Page={page}&PageSize={pageSize}");

    public async ValueTask<ApiResponse<Paged<ChatSessionResponse>>> SearchChatSessions(string query, int page = 1, int pageSize = 0) 
        => await _httpClient.GetAsync<Paged<ChatSessionResponse>>($"/chat-history/sessions/search?query={query}&Page={page}&PageSize={pageSize}");

    public async ValueTask<ApiResponse<ChatSessionResponse>> GetChatSession(Guid id) 
        => await _httpClient.GetAsync<ChatSessionResponse>($"/chat-history/sessions/{id}");

    public async ValueTask<ApiResponse<Paged<ChatMessageResponse>>> GetChatMessagesByChatId(Guid chatId, int page = 1, int pageSize = 0) 
        => await _httpClient.GetAsync<Paged<ChatMessageResponse>>($"/chat-history/sessions/{chatId}/messages?Page={page}&PageSize={pageSize}");

    public async ValueTask<ApiResponse> ChangeChatSessionTitle(Guid id, string title)
    {
        var payload = new ChangeChatSessionTitleRequest(id, title);

        var response = await _httpClient.PutAsync($"/chat-history/sessions/{id}/title", payload);

        return response;
    }

    public async ValueTask<ApiResponse<Guid>> CreateChatSession(Guid assistantId, string title, string message)
    {
        var payload = new CreateChatSessionRequest(null, assistantId, title, message);

        var response = await _httpClient.PostAsync<Guid>($"/chat-history/sessions", payload);

        return response;
    }

    public async ValueTask<ApiResponse> DeleteChatSession(Guid id) 
        => await _httpClient.DeleteAsync($"/chat-history/sessions/{id}");

    public async ValueTask<ApiResponse> SendMessage(Guid chatId, string message, string messageType, string model)
    {
        var payload = new AskRequest(chatId, message, messageType, model);

        var response = await _httpClient.PostAsync($"/chats/{chatId}/messages", payload);

        return response;
    }
}
