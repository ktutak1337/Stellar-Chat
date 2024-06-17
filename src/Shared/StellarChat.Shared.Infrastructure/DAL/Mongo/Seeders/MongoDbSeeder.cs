using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo.Seeders;

internal class MongoDbSeeder : IMongoDbSeeder
{
    private readonly IAppSettingsSeeder _appSettingsSeeder;
    private readonly IAssistantsSeeder _assistantsSeeder;
    private readonly IActionsSeeder _actionsSeeder;
    private readonly ILogger<MongoDbSeeder> _logger;

    public MongoDbSeeder(IAppSettingsSeeder appSettingsSeeder, IAssistantsSeeder assistantsSeeder, IActionsSeeder actionsSeeder, ILogger<MongoDbSeeder> logger)
    {
        _appSettingsSeeder = appSettingsSeeder;
        _assistantsSeeder = assistantsSeeder;
        _actionsSeeder = actionsSeeder;
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
        await _assistantsSeeder.SeedAsync(database);
        await _actionsSeeder.SeedAsync(database);

        _logger.LogInformation("Finished seeding the database.");
    }
}
