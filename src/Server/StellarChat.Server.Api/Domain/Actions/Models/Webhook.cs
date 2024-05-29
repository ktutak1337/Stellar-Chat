using StellarChat.Server.Api.Domain.Actions.Exceptions;

namespace StellarChat.Server.Api.Domain.Actions.Models;

internal class Webhook
{
    private static readonly List<string> AllowedHttpMethods = ["GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS"];

    public string HttpMethod { get; set; }
    public string Url { get; set; }
    public string? Payload { get; set; }
    public bool IsRetryEnabled { get; set; }
    public int RetryCount { get; set; }
    public TimeSpan RetryInterval { get; set; }
    public bool IsScheduled { get; set; }
    public string? CronExpression { get; set; }
    public Dictionary<string, string>? Headers { get; set; }

    public Webhook(
        string httpMethod, 
        string url, 
        string? payload, 
        bool isRetryEnabled, 
        int retryCount, 
        TimeSpan retryInterval, 
        bool isScheduled, 
        string? cronExpression, 
        Dictionary<string, string>? headers = null)
    {
        HttpMethod = EnsureValidHttpMethod(httpMethod);
        Url = EnsureValidUrl(url);
        Payload = payload ?? string.Empty;
        IsRetryEnabled = isRetryEnabled;
        RetryCount = retryCount;
        RetryInterval = retryInterval;
        IsScheduled = isScheduled;
        CronExpression = cronExpression ?? string.Empty;
        Headers = headers ?? [];
    }

    public static Webhook Create(
        string httpMethod, 
        string url, 
        string? payload, 
        bool isRetryEnabled, 
        int retryCount, 
        TimeSpan retryInterval, 
        bool isScheduled, 
        string? cronExpression, 
        Dictionary<string, string>? headers = null) 
            => new(httpMethod, url, payload, isRetryEnabled, retryCount, retryInterval, isScheduled, cronExpression, headers);

    public void SetApiKey(string apiKey)
    {
        if(apiKey.IsNotEmpty() && Headers is not null)
        {
            Headers["Authorization"] = $"Bearer {apiKey}";
        }
    }

    private static string EnsureValidHttpMethod(string httpMethod)
    {
        httpMethod = httpMethod.ToUpper();

        if (!AllowedHttpMethods.Contains(httpMethod))
        {
            throw new InvalidHttpMethodException(httpMethod, AllowedHttpMethods);
        }

        return httpMethod;
    }

    private static string EnsureValidUrl(string url)
    {
        if (url.IsNotEmpty())
        {
            url = url.ToLowerInvariant();

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new InvalidUrlAddressException(url);
            }
        }

        return url.IsEmpty() ? string.Empty : url;
    }
}
