namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

internal class MongoOptions
{
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
    public bool DisableTransactions { get; set; }
}
