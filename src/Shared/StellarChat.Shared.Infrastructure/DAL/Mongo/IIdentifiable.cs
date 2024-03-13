namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public interface IIdentifiable<out T>
{
    T Id { get; }
}
