namespace StellarChat.Server.Api.Domain.Settings.Models;

public class AppSettings
{
    public Guid Id { get; set; }
    public string Key { get; set; } = "app-settings";
    public Profile Profile { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public AppSettings(Guid id, string key, Profile profile, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        Key = key;
        Profile = profile;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
