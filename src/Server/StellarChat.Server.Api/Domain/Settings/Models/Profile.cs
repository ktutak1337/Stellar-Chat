namespace StellarChat.Server.Api.Domain.Settings.Models;

public class Profile(string name, string avatarUrl, string description)
{
    public string Name { get; set; } = name;
    public string AvatarUrl { get; set; } = avatarUrl;
    public string Description { get; set; } = description;
}
