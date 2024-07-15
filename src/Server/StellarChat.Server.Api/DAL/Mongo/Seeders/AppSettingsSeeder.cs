namespace StellarChat.Server.Api.DAL.Mongo.Seeders;

public class AppSettingsSeeder(ILogger<AppSettingsSeeder> logger, TimeProvider clock) : IAppSettingsSeeder
{
    private readonly ILogger<AppSettingsSeeder> _logger = logger;
    private readonly TimeProvider _clock = clock;

    public async Task SeedAsync(IMongoDatabase database)
    {
        var settingsCollection = database.GetCollection<AppSettingsDocument>("settings");
        var cursor = await settingsCollection.FindAsync(FilterDefinition<AppSettingsDocument>.Empty);
        var documents = await cursor.ToListAsync();

        if (documents.Any())
        {
            return;
        }

        var now = _clock.GetLocalNow();

        var appSettingsDocument = new AppSettingsDocument
        {
            Id = Guid.NewGuid(),
            Profile = new ProfileDocument
            {
                Name = "User",
                AvatarUrl = "https://raw.githubusercontent.com/ktutak1337/Stellar-Chat/main/docs/assets/logo-small.jpg",
                Description = string.Empty,
            },
            Integrations =
            [
                new() {
                    Name = "OpenAI",
                    ApiKey = string.Empty,
                    Endpoint = string.Empty,
                    IsEnabled = false,
                },
                new() {
                    Name = "Ollama",
                    ApiKey = string.Empty,
                    Endpoint = string.Empty,
                    IsEnabled = false,
                },
            ],
            CreatedAt = now,
            UpdatedAt = now
        };

        _logger.LogInformation("Started seeding 'settings' collection.");

        await settingsCollection.InsertOneAsync(appSettingsDocument);

        _logger.LogInformation($"Added a settings document to the database with 'ID': {appSettingsDocument.Id}, and 'Key': {appSettingsDocument.Key}.");
        _logger.LogInformation("Finished seeding 'settings' collection.");
    }
}
