namespace StellarChat.Server.Api.DAL.Mongo.Documents.Settings;

internal sealed class AppSettingsDocument(Guid id, ProfileDocument profile, DateTimeOffset createdAt, DateTimeOffset updatedAt) : IIdentifiable<Guid>
{
    public Guid Id { get; set; } = id;
    public string Key { get; set; } = "app-settings";
    public ProfileDocument Profile { get; set; } = profile;
    public DateTimeOffset CreatedAt { get; set; } = createdAt;
    public DateTimeOffset UpdatedAt { get; set; } = updatedAt;
}
