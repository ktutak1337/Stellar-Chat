namespace StellarChat.Server.Api.Features.Models.Providers;

internal interface IModelsProvider
{
    string ProviderName { get; }
    ValueTask<IEnumerable<AvailableModelsResponse>> FetchModelsAsync(BrowseAvailableModels.BrowseAvailableModels query, CancellationToken cancellationToken = default);
    IEnumerable<AvailableModelsResponse> FilterModels(string filter, IEnumerable<AvailableModelsResponse> models);
}
