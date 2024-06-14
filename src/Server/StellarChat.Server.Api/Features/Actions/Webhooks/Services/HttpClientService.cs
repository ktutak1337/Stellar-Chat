using Microsoft.AspNetCore.WebUtilities;
using StellarChat.Server.Api.Features.Actions.Webhooks.Services.Constants;
using System.Text;

namespace StellarChat.Server.Api.Features.Actions.Webhooks.Services;

internal sealed class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientService(IHttpClientFactory httpClientFactory) 
        => _httpClientFactory = httpClientFactory;

    public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? headers, CancellationToken cancellationToken) 
        => await SendRequestAsync(url, HttpMethod.Get, null, headers, cancellationToken);

    public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? queryParams, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        if (queryParams is not null && queryParams.Count > 0)
        {
            url = QueryHelpers.AddQueryString(url, queryParams!);
        }

        return await SendRequestAsync(url, HttpMethod.Get, null, headers, cancellationToken);
    }

    public async Task<HttpResponseMessage> PostAsync(string url, string body, Dictionary<string, string>? headers, CancellationToken cancellationToken = default)
        => await SendRequestAsync(url, HttpMethod.Post, CreateJsonContent(body), headers, cancellationToken);

    public async Task<HttpResponseMessage> PutAsync(string url, string body, Dictionary<string, string>? headers, CancellationToken cancellationToken = default)
        => await SendRequestAsync(url, HttpMethod.Put, CreateJsonContent(body), headers, cancellationToken);

    public async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string>? headers, CancellationToken cancellationToken = default)
        => await SendRequestAsync(url, HttpMethod.Delete, null, headers, cancellationToken);

    private async Task<HttpResponseMessage> SendRequestAsync(string url, HttpMethod httpMethod, HttpContent? content, Dictionary<string, string>? headers, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient("Webhooks");

        using var request = new HttpRequestMessage(httpMethod, url) { Content = content };

        request.Headers.Add(HttpHeaderConstant.UserAgentKey, HttpHeaderConstant.UserAgentValue);

        if (headers is not null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var response = await httpClient.SendAsync(request, cancellationToken);
        
        return response;
    }

    private static HttpContent CreateJsonContent(string jsonData) 
        => new StringContent(jsonData, Encoding.UTF8, "application/json");
}
