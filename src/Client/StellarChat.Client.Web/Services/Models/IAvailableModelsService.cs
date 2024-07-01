using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Models;

namespace StellarChat.Client.Web.Services.Models;

public interface IAvailableModelsService
{
    ValueTask<ApiResponse<IEnumerable<AvailableModelsResponse>>> BrowseAvailableModels();
}
