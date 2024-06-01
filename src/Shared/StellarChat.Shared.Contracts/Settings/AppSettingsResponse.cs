namespace StellarChat.Shared.Contracts.Settings;

public class AppSettingsResponse
{
    public Guid Id { get; set; }
    public string Key { get; set; } = "app-settings";
    public Profile Profile { get; set; } = new();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
