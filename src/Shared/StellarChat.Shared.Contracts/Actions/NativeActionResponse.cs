namespace StellarChat.Shared.Contracts.Actions;

public class NativeActionResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Metaprompt { get; set; } = string.Empty;
    public bool IsRemoteAction { get; set; }
    public bool ShouldRephraseResponse { get; set; }
    public WebhookResponse? Webhook { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
