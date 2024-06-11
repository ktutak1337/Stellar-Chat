using StellarChat.Shared.Contracts.Actions;

namespace StellarChat.Client.Web.Services.Actions;

public interface IActionService
{
    ValueTask<NativeActionResponse> GetAction(Guid id);
    ValueTask<IEnumerable<NativeActionResponse>> BrowseActions();
    ValueTask<Guid> CreateAction(NativeActionResponse action);
    ValueTask<string> ExecuteAction(Guid id, Guid chatId, string message);
    ValueTask UpdateAction(NativeActionResponse action);
    ValueTask DeleteAction(Guid id);
}
