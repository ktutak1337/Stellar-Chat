namespace StellarChat.Shared.Infrastructure.Identity.Seeders;

public interface IMongoIdentitySeeder
{ 
    Task SeedIdentityAsync();
}
