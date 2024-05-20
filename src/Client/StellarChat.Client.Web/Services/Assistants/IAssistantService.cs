using StellarChat.Client.Web.Shared;
using StellarChat.Shared.Contracts.Assistants;

namespace StellarChat.Client.Web.Services.Assistants;

public interface IAssistantService
{
    ValueTask<Paged<AssistantResponse>> BrowseAssistants(int page = 0, int pageSize = 10000);
}
