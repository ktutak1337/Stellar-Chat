using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Assistants;
using System.Net.Http.Json;

namespace StellarChat.Client.Web.Services.Assistants;

public class AssistantService : IAssistantService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AssistantService(IHttpClientFactory httpClientFactory) 
        => _httpClientFactory = httpClientFactory;

    public async ValueTask<Paged<AssistantResponse>> BrowseAssistants(int page = 0, int pageSize = 10000)
    {
        var httpClient = _httpClientFactory.CreateClient("WebAPI");

        var result = await httpClient.GetFromJsonAsync<Paged<AssistantResponse>>($"/assistants?Page={page}&PageSize={pageSize}");

        return result!;
    }
}
