using StellarChat.Client.Web.Shared.Http;
using StellarChat.Shared.Contracts.Actions;

namespace StellarChat.Client.Web.Services.Actions;

public class ActionService(IRestHttpClient httpClient) : IActionService
{
    private readonly IRestHttpClient _httpClient = httpClient;

    public async ValueTask<ApiResponse<NativeActionResponse>> GetAction(Guid actionId) 
        => await _httpClient.GetAsync<NativeActionResponse>($"/actions/{actionId}");

    public async ValueTask<ApiResponse<IEnumerable<NativeActionResponse>>> BrowseActions()
        => await _httpClient.GetAsync<IEnumerable<NativeActionResponse>>($"/actions");

    public async ValueTask<ApiResponse<Guid>> CreateAction(NativeActionResponse action)
    {
        var payload = new CreateNativeActionRequest(
            action.Id,
            action.Name,
            action.Category,
            action.Icon,
            action.Model,
            action.Metaprompt,
            action.IsSingleMessageMode,
            action.IsRemoteAction,
            action.ShouldRephraseResponse,
            new Webhook(
                action.Webhook!.HttpMethod,
                action.Webhook!.Url,
                action.Webhook.Payload,
                action.Webhook.IsRetryEnabled,
                action.Webhook.RetryCount,
                action.Webhook.RetryInterval,
                action.Webhook.IsScheduled,
                action.Webhook.CronExpression,
                action.Webhook.Headers));

        var response = await _httpClient.PostAsync<Guid>($"/actions", payload);

        return response;
    }

    public async ValueTask<ApiResponse<string>> ExecuteAction(Guid actionId, Guid chatId, string serviceId, string message)
    {
        var payload = new ExecuteNativeActionRequest(actionId, chatId, serviceId, message);
        
        var response = await _httpClient.PostAsync<string>($"/actions/{actionId}/execute", payload);

        return response;
    }

    public async ValueTask<ApiResponse> UpdateAction(NativeActionResponse action)
    {
        var actionId = action.Id;

        var payload = new UpdateNativeActionRequest(
            actionId,
            action.Name,
            action.Category,
            action.Icon,
            action.Model,
            action.Metaprompt,
            action.IsSingleMessageMode,
            action.IsRemoteAction,
            action.ShouldRephraseResponse,
            new Webhook(
                action.Webhook!.HttpMethod,
                action.Webhook!.Url,
                action.Webhook.Payload,
                action.Webhook.IsRetryEnabled,
                action.Webhook.RetryCount,
                action.Webhook.RetryInterval,
                action.Webhook.IsScheduled,
                action.Webhook.CronExpression,
                action.Webhook.Headers));

        var response = await _httpClient.PutAsync($"/actions/{actionId}", payload);

        return response;
    }

    public async ValueTask<ApiResponse> DeleteAction(Guid actionId) 
        => await _httpClient.DeleteAsync($"/actions/{actionId}");
}
