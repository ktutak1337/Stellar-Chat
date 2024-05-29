using StellarChat.Server.Api.Domain.Actions.Models;

namespace StellarChat.Server.Api.Domain.Actions.Repositories;

internal interface INativeActionRepository
{
    ValueTask<NativeAction?> GetAsync(Guid id);
    ValueTask<IEnumerable<NativeAction>> BrowseAsync();
    ValueTask AddAsync(NativeAction nativeAction);
    ValueTask UpdateAsync(NativeAction nativeAction);
    ValueTask DeleteAsync(Guid id);
    ValueTask<bool> ExistsAsync(Guid id);
}
