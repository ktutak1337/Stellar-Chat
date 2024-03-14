using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo.Seeders;

internal class MongoDbSeeder : IMongoDbSeeder
{
    private readonly ILogger<MongoDbSeeder> _logger;

    public MongoDbSeeder(ILogger<MongoDbSeeder> logger)
    {
        _logger = logger;
    }

    public async Task SeedAsync(IMongoDatabase database)
    {
        await CustomSeedAsync(database);
    }

    protected virtual async Task CustomSeedAsync(IMongoDatabase database)
    {

    }
}
