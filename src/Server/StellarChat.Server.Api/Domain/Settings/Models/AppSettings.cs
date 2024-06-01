namespace StellarChat.Server.Api.Domain.Settings.Models;

public class AppSettings(Guid id, Profile profile, DateTimeOffset createdAt, DateTimeOffset updatedAt)
{
    public Guid Id { get; set; } = id;
    public string Key { get; set; } = "app-settings";
    public Profile Profile { get; set; } = profile;
    public DateTimeOffset CreatedAt { get; set; } = createdAt;
    public DateTimeOffset UpdatedAt { get; set; } = updatedAt;
}
