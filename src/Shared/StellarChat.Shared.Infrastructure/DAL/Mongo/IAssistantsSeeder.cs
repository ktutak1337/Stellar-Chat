using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public interface IAssistantsSeeder
{
    Task SeedAsync(IMongoDatabase database);
}
