using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync(IMongoDatabase database);
    }
}
