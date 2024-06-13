using System.ComponentModel.DataAnnotations;

namespace StellarChat.Shared.Contracts.Actions;

public class WebhookResponse
{
    [Required, MinLength(3), MaxLength(7)] public string HttpMethod { get; set; } = string.Empty;
    [Required] public string Url { get; set; } = string.Empty;
    [Required] public string? Payload { get; set; }
    public bool IsRetryEnabled { get; set; }
    public int RetryCount { get; set; }
    public int RetryInterval { get; set; }
    public bool IsScheduled { get; set; }
    public string? CronExpression { get; set; }
    public Dictionary<string, string>? Headers { get; set; }
}
