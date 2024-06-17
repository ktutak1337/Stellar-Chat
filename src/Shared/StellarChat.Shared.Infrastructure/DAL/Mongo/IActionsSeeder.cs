using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public interface IActionsSeeder
{
    Task SeedAsync(IMongoDatabase database);
}
