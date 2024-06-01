namespace StellarChat.Server.Api.DAL.Mongo.Documents.Settings;

internal sealed class ProfileDocument
{
    public string Name { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
