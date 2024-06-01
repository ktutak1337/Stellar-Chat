namespace StellarChat.Shared.Contracts.Models;

public class AvailableModelsResponse
{
    public string Name { get; set; } = string.Empty;
    public int ContextSize { get; set; }
    public string Vendor { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
}
