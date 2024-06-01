namespace StellarChat.Server.Api.Features.Models.Providers;

internal interface IModelsProvider
{
    ValueTask<IEnumerable<AvailableModelsResponse>> FetchModelsAsync(CancellationToken cancellationToken = default);
}
