namespace StellarChat.Client.Web.Models;

public class Assistant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Metaprompt { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string DefaultModel { get; set; } = string.Empty;
    public string DefaultVoice { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
