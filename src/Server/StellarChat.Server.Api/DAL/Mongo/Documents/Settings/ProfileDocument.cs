namespace StellarChat.Server.Api.DAL.Mongo.Documents.Settings;

internal sealed class ProfileDocument(string name, string avatarUrl, string description)
{
    public string Name { get; set; } = name;
    public string AvatarUrl { get; set; } = avatarUrl;
    public string Description { get; set; } = description;
}
