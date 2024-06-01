namespace StellarChat.Server.Api.Domain.Settings.Models;

public class Profile
{
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public string Description { get; set; }

    public Profile(string name, string avatarUrl, string description)
    {
        Name = name;
        AvatarUrl = avatarUrl;
        Description = description;
    }
}
