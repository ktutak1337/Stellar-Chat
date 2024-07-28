namespace StellarChat.Server.Api.Features.Models.BrowseModelsCatalog.Catalogs;

internal interface IModelCatalog
{
    string ProviderName { get; }
    ValueTask<IEnumerable<ModelCatalog>> FetchModelsAsync(BrowseModelsCatalog query, CancellationToken cancellationToken = default);
    IEnumerable<ModelCatalog> FilterModels(string filter, IEnumerable<ModelCatalog> models);
}
