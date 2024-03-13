using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo.Seeders;

internal class MongoDbSeeder : IMongoDbSeeder
{
    private readonly IAppSettingsSeeder _appSettingsSeeder;
    private readonly ILogger<MongoDbSeeder> _logger;

    public MongoDbSeeder(IAppSettingsSeeder appSettingsSeeder, ILogger<MongoDbSeeder> logger)
    {
        _appSettingsSeeder = appSettingsSeeder;
        _logger = logger;
    }

    public async Task SeedAsync(IMongoDatabase database)
    {
        await CustomSeedAsync(database);
    }

    protected virtual async Task CustomSeedAsync(IMongoDatabase database)
    {
        _logger.LogInformation("Started seeding the database.");

        await _appSettingsSeeder.SeedAsync(database);

        _logger.LogInformation("Finished seeding the database.");
    }
}
