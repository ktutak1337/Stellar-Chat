namespace StellarChat.Server.Api.DAL.Mongo.Documents.Actions;

public class NativeActionDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Metaprompt { get; set; } = string.Empty;
    public bool IsSingleMessageMode { get; set; }
    public bool IsRemoteAction { get; set; }
    public bool ShouldRephraseResponse { get; set; }
    public WebhookDocument? Webhook { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
