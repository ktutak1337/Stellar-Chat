using StellarChat.Shared.Infrastructure.Identity;
using StellarChat.Shared.Infrastructure.Identity.Seeders;

namespace StellarChat.Server.Api.DAL.Mongo.Seeders;

internal sealed class RolesSeeder(RoleManager<ApplicationRole> roleManager, ILogger<RolesSeeder> logger) : IRolesSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly ILogger<RolesSeeder> _logger = logger;

    public async Task SeedAsync()
    {
        var role1 = new ApplicationRole
        {
            Name = StellarRoles.Basic
        };

        _logger.LogInformation("Started seeding 'roles' collection.");

        if (!await _roleManager.RoleExistsAsync(role1.Name))
        {
            var result1 = await _roleManager.CreateAsync(role1);

            if (result1.Succeeded)
                _logger.LogInformation($"Added a role to the database with 'ID': {role1.Id}, and 'Name': {role1.Name}.");
        }

        _logger.LogInformation("Finished seeding 'roles' collection.");
    }
}