using System.Text.Json;
using System.Text;

namespace StellarChat.Client.Web.Shared.Http;

public class RestHttpClient(IHttpClientFactory httpClientFactory, ILogger<RestHttpClient> logger) : IRestHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<RestHttpClient> _logger = logger;

    private const string HttpClientName = "WebAPI";

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        => TryRequestAsync<T>(new HttpRequestMessage(HttpMethod.Get, endpoint));

    public async Task<ApiResponse> PostAsync(string endpoint, object request)
    {
        var response = await TryRequestAsync<object>(new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = GetPayload(request)
        });

        return response;
    }

    public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object request)
    {
        var response = await TryRequestAsync<T>(new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = GetPayload(request)
        });

        return response;
    }

    public async Task<ApiResponse> PutAsync(string endpoint, object request)
    {
        var response = await TryRequestAsync<object>(new HttpRequestMessage(HttpMethod.Put, endpoint)
        {
            Content = GetPayload(request)
        });

        return response;
    }

    public async Task<ApiResponse> DeleteAsync(string endpoint)
    {
        var response = await TryRequestAsync<object>(new HttpRequestMessage(HttpMethod.Delete, endpoint));

        return response;
    }

    private static StringContent GetPayload<T>(T request)
        => new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

    private async Task<ApiResponse<T>> TryRequestAsync<T>(HttpRequestMessage request)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        HttpResponseMessage? response = null;

        try
        {
            var requestId = Guid.NewGuid().ToString("N");
            _logger.LogInformation($"Started processing a request [Request ID]: '{requestId}'");

            response = await httpClient.SendAsync(request);
            
            var isValid = response.IsSuccessStatusCode;
            var payload = await response.Content.ReadAsStringAsync();
            
            if (!isValid)
            {
                var errors = DeserializeResponse<ErrorsResponse>(payload);
                
                _logger.LogError(response.ToString());
                _logger.LogError(payload);

                return new ApiResponse<T>(default!, response, false, errors);
            }

            var result = DeserializeResponse<T>(payload);

            _logger.LogInformation($"Finished processing a request with status code: {(int)response.StatusCode} [Request ID]: '{requestId}'");

            return new ApiResponse<T>(result!, response, true);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new ApiResponse<T>(default!, response!, false);
        }
    }

    private T DeserializeResponse<T>(string payload)
    {
        var content = payload.IsEmpty()
            ? default
            : JsonSerializer.Deserialize<T>(payload, SerializerOptions);

        return content!;
    }
}
