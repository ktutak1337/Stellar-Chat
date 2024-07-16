using Microsoft.Extensions.Caching.Memory;
using StellarChat.Server.Api.Features.Models.Providers;
using System.Collections.Concurrent;
using System.Text;

namespace StellarChat.Server.Api.Features.Models.BrowseAvailableModels;

internal sealed class BrowseAvailableModelsHandler : IQueryHandler<BrowseAvailableModels, IEnumerable<AvailableModelsResponse>?>
{
    private readonly IEnumerable<IModelsProvider> _modelProviders;
    private readonly IMemoryCache _memoryCache;

    private readonly ConcurrentBag<string> _activeCacheKeys = new();

    public BrowseAvailableModelsHandler(IEnumerable<IModelsProvider> modelProviders, IMemoryCache memoryCache)
    {
        _modelProviders = modelProviders;
        _memoryCache = memoryCache;
    }

    public async ValueTask<IEnumerable<AvailableModelsResponse>?> Handle(BrowseAvailableModels query, CancellationToken cancellationToken)
    {
        var cacheKey = GenerateCacheKey(query);
        _activeCacheKeys.Add(cacheKey);

        return await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

            return await FetchModelsFromProvider(query, cancellationToken) ?? [];
        });
    }

    private async Task<IEnumerable<AvailableModelsResponse>> FetchModelsFromProvider(BrowseAvailableModels query, CancellationToken cancellationToken)
    {
        var availableModels = new List<AvailableModelsResponse>();

        foreach (var provider in _modelProviders)
        {
            var providerName = provider.ProviderName;
            var models = await provider.FetchModelsAsync(query, cancellationToken);

            if (query.Provider is not null 
                && query.Provider.Equals(providerName, StringComparison.OrdinalIgnoreCase) 
                && query.Filter is not null)
            {
                models = provider.FilterModels(query.Filter, models);
            }

            availableModels.AddRange(models);
        }
        return availableModels;
    }

    private string GenerateCacheKey(BrowseAvailableModels query)
    {
        var keyBuilder = new StringBuilder("AvailableModels");
        
        if (query.Provider != null) keyBuilder.Append($"_{query.Provider}");
        if (query.Filter != null) keyBuilder.Append($"_{query.Filter}");

        var cacheKey = keyBuilder.ToString();
        
        return cacheKey;
    }
}
