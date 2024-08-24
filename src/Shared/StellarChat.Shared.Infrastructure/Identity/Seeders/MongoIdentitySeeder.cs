using Microsoft.Extensions.Logging;

namespace StellarChat.Shared.Infrastructure.Identity.Seeders;

public class MongoIdentitySeeder : IMongoIdentitySeeder
{ 
    private readonly IRolesSeeder _rolesSeeder;
    private readonly IUsersSeeder _usersSeeder; 
    private readonly ILogger<MongoIdentitySeeder> _logger;

    public MongoIdentitySeeder(IRolesSeeder rolesSeeder, IUsersSeeder usersSeeder, ILogger<MongoIdentitySeeder> logger)
    { 
        _usersSeeder = usersSeeder;
        _rolesSeeder = rolesSeeder;
        _logger = logger;
    } 
    /// <summary>
    /// Seed Identity entities: roles, users..
    /// !!! This method must be called after AddMongoIdentity, actually after registration identity manager services.
    /// </summary>
    /// <returns></returns>
    public async Task SeedIdentityAsync()
    {
        _logger.LogInformation("Started Identity seeding the database.");

        await _rolesSeeder.SeedAsync();
        await _usersSeeder.SeedAsync(); 

        _logger.LogInformation("Finished Identity seeding the database.");
    }
     
}
