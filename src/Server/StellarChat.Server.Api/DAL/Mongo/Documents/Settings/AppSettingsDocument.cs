namespace StellarChat.Server.Api.DAL.Mongo.Documents.Settings;

internal sealed class AppSettingsDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Key { get; set; } = "app-settings";
    public ProfileDocument? Profile { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
