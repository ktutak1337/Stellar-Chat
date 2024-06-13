using System.ComponentModel.DataAnnotations;

namespace StellarChat.Shared.Contracts.Actions;

public class NativeActionResponse
{
    public Guid Id { get; set; }
    [Required, MinLength(3)] public string Name { get; set; } = string.Empty;
    [Required, MinLength(3)] public string Category { get; set; } = string.Empty;
    [Required] public string Icon { get; set; } = string.Empty;
    [Required] public string Model { get; set; } = string.Empty;
    [Required, MinLength(3)] public string Metaprompt { get; set; } = string.Empty;
    public bool IsRemoteAction { get; set; }
    public bool ShouldRephraseResponse { get; set; }
    public WebhookResponse? Webhook { get; set; } = new();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
