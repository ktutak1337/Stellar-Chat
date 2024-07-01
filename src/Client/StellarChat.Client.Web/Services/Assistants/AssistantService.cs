using StellarChat.Client.Web.Shared;
using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Assistants;

namespace StellarChat.Client.Web.Services.Assistants;

public class AssistantService(IRestHttpClient httpClient) : IAssistantService
{

    private readonly IRestHttpClient _httpClient = httpClient;

    public async ValueTask<ApiResponse<AssistantResponse>> GetAssistant(Guid id) 
        => await _httpClient.GetAsync<AssistantResponse>($"/assistants/{id}");

    public async ValueTask<ApiResponse<Paged<AssistantResponse>>> BrowseAssistants(int page = 1, int pageSize = 0)
        => await _httpClient.GetAsync<Paged<AssistantResponse>>($"/assistants?Page={page}&PageSize={pageSize}");

    public async ValueTask<ApiResponse> CreateAssistant(AssistantResponse assistant)
    {
        var payload = new CreateAssistantRequest(
            assistant.Id, 
            assistant.Name, 
            assistant.Metaprompt, 
            assistant.Description, 
            assistant.AvatarUrl, 
            assistant.DefaultModel, 
            assistant.DefaultVoice, 
            assistant.IsDefault);

        var response = await _httpClient.PostAsync($"/assistants", payload);

        return response;
    }

    public async ValueTask<ApiResponse> UpdateAssistant(AssistantResponse assistant)
    {
        var assistantId = assistant.Id;

        var payload = new UpdateAssistantRequest(
            assistantId,
            assistant.Name,
            assistant.Metaprompt,
            assistant.Description,
            assistant.AvatarUrl,
            assistant.DefaultModel,
            assistant.DefaultVoice,
            assistant.IsDefault);

        var response = await _httpClient.PutAsync($"/assistants/{assistantId}", payload);

        return response;
    }

    public async ValueTask<ApiResponse> SetDefaultAssistant(Guid id, bool isDefault)
    {
        var payload = new SetDefaultAssistantRequest(id, isDefault);

        var response = await _httpClient.PutAsync($"/assistants/{id}/default", payload);

        return response;
    }

    public async ValueTask<ApiResponse> DeleteAssistant(Guid id)
        => await _httpClient.DeleteAsync($"/assistants/{id}");
}
