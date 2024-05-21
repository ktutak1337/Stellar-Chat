using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Assistants;
using System.Net.Http.Json;

namespace StellarChat.Client.Web.Services.Assistants;

public class AssistantService : IAssistantService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AssistantService(IHttpClientFactory httpClientFactory) 
        => _httpClientFactory = httpClientFactory;


    public async ValueTask<AssistantResponse> GetAssistant(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var result = await httpClient.GetFromJsonAsync<AssistantResponse>($"/assistants/{id}");

        return result!;
    }

    public async ValueTask<Paged<AssistantResponse>> BrowseAssistants(int page = 0, int pageSize = 10000)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var result = await httpClient.GetFromJsonAsync<Paged<AssistantResponse>>($"/assistants?Page={page}&PageSize={pageSize}");

        return result!;
    }

    public async ValueTask CreateAssistant(AssistantResponse assistant)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var(id, name, metaprompt, description, avatarUrl, defaultModel, defaultVoice, isDefault, _, _) = assistant;

        var payload = new CreateAssistantRequest(id, name, metaprompt, description, avatarUrl, defaultModel, defaultVoice, isDefault);

        await httpClient.PostAsJsonAsync($"/assistants", payload);
    }

    public async ValueTask UpdateAssistant(AssistantResponse assistant)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var (id, name, metaprompt, description, avatarUrl, defaultModel, defaultVoice, isDefault, _, _) = assistant;

        var payload = new UpdateAssistantRequest(id, name, metaprompt, description, avatarUrl, defaultModel, defaultVoice, isDefault);

        await httpClient.PutAsJsonAsync($"/assistants/{id}", payload);
    }
}
