using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Assistants;

namespace StellarChat.Client.Web.Services.Assistants;

public interface IAssistantService
{
    ValueTask<AssistantResponse> GetAssistant(Guid id);
    ValueTask<Paged<AssistantResponse>> BrowseAssistants(int page = 0, int pageSize = 10000);
    ValueTask CreateAssistant(AssistantResponse assistant);
    ValueTask UpdateAssistant(AssistantResponse assistant);
    ValueTask SetDefaultAssistant(Guid id, bool isDefault);
}
