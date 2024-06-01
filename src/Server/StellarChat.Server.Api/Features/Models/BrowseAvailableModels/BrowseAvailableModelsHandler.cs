using Microsoft.Extensions.Caching.Memory;
using StellarChat.Server.Api.Features.Models.Providers;

namespace StellarChat.Server.Api.Features.Models.BrowseAvailableModels;

internal sealed class BrowseAvailableModelsHandler : IQueryHandler<BrowseAvailableModels, IEnumerable<AvailableModelsResponse>?>
{
    private const string CacheKey = "AvailableModels";

    private readonly IEnumerable<IModelsProvider> _modelProviders;
    private readonly IMemoryCache _memoryCache;

    public BrowseAvailableModelsHandler(IEnumerable<IModelsProvider> modelProviders, IMemoryCache memoryCache)
    {
        _modelProviders = modelProviders;
        _memoryCache = memoryCache;
    }

    public async ValueTask<IEnumerable<AvailableModelsResponse>?> Handle(BrowseAvailableModels query, CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(CacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

            return await FetchModelsFromProvider(cancellationToken) ?? [];
        });
    }

    private async Task<IEnumerable<AvailableModelsResponse>> FetchModelsFromProvider(CancellationToken cancellationToken)
    {
        var availableModels = new List<AvailableModelsResponse>();
        
        foreach (var provider in _modelProviders)
        {
            var models = await provider.FetchModelsAsync(cancellationToken);
            availableModels.AddRange(models);
        }

        return availableModels;
    }
}


