using StellarChat.Shared.Contracts.Models;

namespace StellarChat.Client.Web.Services.Models;

public interface IAvailableModelsService
{
    ValueTask<IEnumerable<AvailableModelsResponse>> BrowseAvailableModels();
}
