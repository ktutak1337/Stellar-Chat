using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public interface IAppSettingsSeeder
{
    Task SeedAsync(IMongoDatabase database);
}
