namespace StellarChat.Server.Api.Features.Models.BrowseModelsCatalog;

internal sealed record BrowseModelsCatalog : IQuery<IEnumerable<ModelCatalogResponse>>
{
    public string? Provider { get; set; }
    public string? Filter { get; set; }
}
