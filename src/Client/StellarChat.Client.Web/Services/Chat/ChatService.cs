using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Chat;
using System.Net.Http.Json;

namespace StellarChat.Client.Web.Services.Chat;

public class ChatService : IChatService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ChatService(IHttpClientFactory httpClientFactory) 
        => _httpClientFactory = httpClientFactory;

    public async ValueTask<Paged<ChatSessionResponse>> BrowseChatSessions(int page = 0, int pageSize = 0)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var result = await httpClient.GetFromJsonAsync<Paged<ChatSessionResponse>>($"/chat-history/sessions?Page={page}&PageSize={pageSize}");

        return result!;
    }

    public async ValueTask<Paged<ChatMessageResponse>> GetChatMessagesByChatId(Guid chatId, int page = 0, int pageSize = 10000)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");
        
        var result = await httpClient.GetFromJsonAsync<Paged<ChatMessageResponse>>($"/chat-history/sessions/{chatId}/messages?Page={page}&PageSize={pageSize}");

        return result!;
    }

    public async ValueTask ChangeChatSessionTitle(Guid id, string title)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var payload = new ChangeChatSessionTitleRequest(id, title);

        await httpClient.PutAsJsonAsync($"/chat-history/sessions/{id}/title", payload);
    }

    public async ValueTask DeleteChatSession(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        await httpClient.DeleteAsync($"/chat-history/sessions/{id}");
    }
}
