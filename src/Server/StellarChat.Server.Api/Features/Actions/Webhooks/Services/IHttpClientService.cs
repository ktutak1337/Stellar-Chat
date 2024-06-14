namespace StellarChat.Server.Api.Features.Actions.Webhooks.Services;

internal interface IHttpClientService
{
    Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? headers, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? queryParams, Dictionary<string, string>? headers, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(string url, string body, Dictionary<string, string>? headers, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> PutAsync(string url, string body, Dictionary<string, string>? headers, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string>? headers, CancellationToken cancellationToken = default);
}
