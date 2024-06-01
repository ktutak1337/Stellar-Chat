namespace StellarChat.Server.Api.Domain.Settings.Models;

public class AppSettings(Guid id, Profile profile)
{
    public Guid Id { get; set; } = id;
    public string Key { get; set; } = "app-settings";
    public Profile Profile { get; set; } = profile;
}
