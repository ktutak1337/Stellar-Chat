using StellarChat.Shared.Contracts.Actions;
using System.Net.Http.Json;

namespace StellarChat.Client.Web.Services.Actions;

public class ActionService : IActionService
{
    private const string HttpClientName = "WebAPI";

    private readonly IHttpClientFactory _httpClientFactory;

    public ActionService(IHttpClientFactory httpClientFactory) 
        => _httpClientFactory = httpClientFactory;

    public async ValueTask<NativeActionResponse> GetAction(Guid actionId)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var result = await httpClient.GetFromJsonAsync<NativeActionResponse>($"/actions/{actionId}");

        return result!;
    }

    public async ValueTask<IEnumerable<NativeActionResponse>> BrowseActions()
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var result = await httpClient.GetFromJsonAsync<IEnumerable<NativeActionResponse>>($"/actions");

        return result!;
    }

    public async ValueTask<Guid> CreateAction(NativeActionResponse action)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        
        var payload = new CreateNativeActionRequest(
            action.Id,
            action.Name,
            action.Category,
            action.Icon,
            action.Model,
            action.Metaprompt,
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

        var response = await httpClient.PostAsJsonAsync($"/actions", payload);

        var result = Guid.Empty;

        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<Guid>();
        }

        return result;
    }

    public async ValueTask<string> ExecuteAction(Guid actionId, Guid chatId, string message)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var payload = new ExecuteNativeActionRequest(actionId, chatId, message);

        var response = await httpClient.PostAsJsonAsync($"/actions/{actionId}/execute", payload);

        var result = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<string>();
        }

        return result!;
    }

    public async ValueTask UpdateAction(NativeActionResponse action)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var actionId = action.Id;

        var payload = new UpdateNativeActionRequest(
            actionId,
            action.Name,
            action.Category,
            action.Icon,
            action.Model,
            action.Metaprompt,
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

        await httpClient.PutAsJsonAsync($"/actions/{actionId}", payload);
    }

    public async ValueTask DeleteAction(Guid actionId)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        await httpClient.DeleteAsync($"/actions/{actionId}");
    }
}
