using StellarChat.Client.Web.Shared;
using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Assistants;

namespace StellarChat.Client.Web.Services.Assistants;

public interface IAssistantService
{
    ValueTask<ApiResponse<AssistantResponse>> GetAssistant(Guid id);
    ValueTask<ApiResponse<Paged<AssistantResponse>>> BrowseAssistants(int page = 1, int pageSize = 0);
    ValueTask<ApiResponse> CreateAssistant(AssistantResponse assistant);
    ValueTask<ApiResponse> UpdateAssistant(AssistantResponse assistant);
    ValueTask<ApiResponse> SetDefaultAssistant(Guid id, bool isDefault);
    ValueTask<ApiResponse> DeleteAssistant(Guid id);
}
