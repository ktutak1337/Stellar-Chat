namespace StellarChat.Server.Api.Features.Actions.Webhooks.Services;

internal interface IHttpClientService
{
    Task<string> GetAsync(string url, Dictionary<string, string>? headers, CancellationToken cancellationToken);
    Task<string> GetAsync(string url, Dictionary<string, string>? queryParams, Dictionary<string, string>? headers, CancellationToken cancellationToken);
    Task<string> PostAsync(string url, string body, Dictionary<string, string>? headers, CancellationToken cancellationToken = default);
    Task<string> PutAsync(string url, string body, Dictionary<string, string>? headers, CancellationToken cancellationToken = default);
    Task<string> DeleteAsync(string url, Dictionary<string, string>? headers, CancellationToken cancellationToken = default);
}
