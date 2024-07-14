namespace StellarChat.Server.Api.Domain.Settings.Models;

public class AppSettings
{
    public Guid Id { get; set; }
    public string Key { get; set; } = "app-settings";
    public Profile Profile { get; set; }
    public List<Integration> Integrations { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public AppSettings(Guid id, string key, Profile profile, List<Integration> integrations, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        Key = key;
        Profile = profile;
        Integrations = integrations;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
