namespace StellarChat.Server.Api.Features.Models.BrowseModelsCatalog.Catalogs;

internal interface IModelCatalog
{
    string ProviderName { get; }
    ValueTask<IEnumerable<ModelCatalogResponse>> FetchModelsAsync(BrowseModelsCatalog query, CancellationToken cancellationToken = default);
    IEnumerable<ModelCatalogResponse> FilterModels(string filter, IEnumerable<ModelCatalogResponse> models);
}
