namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

internal class MongoOptions
{
    public string CONNECTION_STRING { get; set; } = null!;
    public string DATABASE { get; set; } = null!;
    public bool DISABLE_TRANSACTIONS { get; set; }
}
