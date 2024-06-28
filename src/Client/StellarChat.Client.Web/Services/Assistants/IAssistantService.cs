using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Assistants;

namespace StellarChat.Client.Web.Services.Assistants;

public interface IAssistantService
{
    ValueTask<AssistantResponse> GetAssistant(Guid id);
    ValueTask<Paged<AssistantResponse>> BrowseAssistants(int page = 0, int pageSize = 10000);
    ValueTask<HttpResponseMessage> CreateAssistant(AssistantResponse assistant);
    ValueTask<HttpResponseMessage> UpdateAssistant(AssistantResponse assistant);
    ValueTask<HttpResponseMessage> SetDefaultAssistant(Guid id, bool isDefault);
    ValueTask<HttpResponseMessage> DeleteAssistant(Guid id);
}
