using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Actions;

namespace StellarChat.Client.Web.Services.Actions;

public interface IActionService
{
    ValueTask<ApiResponse<NativeActionResponse>> GetAction(Guid actionId);
    ValueTask<ApiResponse<IEnumerable<NativeActionResponse>>> BrowseActions();
    ValueTask<ApiResponse<Guid>> CreateAction(NativeActionResponse action);
    ValueTask<ApiResponse<string>> ExecuteAction(Guid actionId, Guid chatId, string message);
    ValueTask<ApiResponse> UpdateAction(NativeActionResponse action);
    ValueTask<ApiResponse> DeleteAction(Guid actionId);
}
