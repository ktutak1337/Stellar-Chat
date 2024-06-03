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

    public async ValueTask<Guid> CreateChatSession(string title)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var payload = new CreateChatSessionRequest(null, title);

        var response = await httpClient.PostAsJsonAsync($"/chat-history/sessions", payload);

        var result = Guid.Empty;

        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<Guid>();
        }

        return result;
    }

    public async ValueTask<HttpResponseMessage> DeleteChatSession(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        return await httpClient.DeleteAsync($"/chat-history/sessions/{id}");
    }

    public async ValueTask SendMessage(Guid chatId, string message, string messageType, string model)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var payload = new AskRequest(chatId, message, messageType, model);

        await httpClient.PostAsJsonAsync($"/chats/{chatId}/messages", payload);
    }
}
