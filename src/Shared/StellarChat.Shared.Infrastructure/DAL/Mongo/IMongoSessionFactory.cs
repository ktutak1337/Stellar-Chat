using MongoDB.Driver;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public interface IMongoSessionFactory
{
    Task<IClientSessionHandle> CreateAsync();
}
