using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Models;

namespace StellarChat.Client.Web.Services.Models;

public interface IModelCatalogService
{
    ValueTask<ApiResponse<IEnumerable<ModelCatalogResponse>>> BrowseModelsCatalog();
}
