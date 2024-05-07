using StellarChat.Server.Api.Domain.Assistants.Models;

namespace StellarChat.Server.Api.Domain.Assistants.Repositories;

internal interface IAssistantRepository
{
    ValueTask<Assistant?> GetAsync(Guid id);
    ValueTask<IEnumerable<Assistant>> BrowseAsync();
    ValueTask AddAsync(Assistant assistant);
    ValueTask UpdateAsync(Assistant assistant);
    ValueTask DeleteAsync(Guid id);
    ValueTask<bool> ExistsAsync(Guid id);
}
