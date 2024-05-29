namespace StellarChat.Server.Api.DAL.Mongo.Documents.Actions;

public class WebhookDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string HttpMethod { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? Payload { get; set; }
    public bool IsRetryEnabled { get; set; }
    public int RetryCount { get; set; }
    public TimeSpan RetryInterval { get; set; }
    public bool IsScheduled { get; set; }
    public string? CronExpression { get; set; }
    public Dictionary<string, string>? Headers { get; set; }
}
