using StellarChat.Shared.Infrastructure.Identity.Seeders;

namespace StellarChat.Server.Api.DAL.Mongo.Seeders;

internal sealed class UsersSeeder(UserManager<ApplicationUser> userManager,ILogger<UsersSeeder> logger) : IUsersSeeder
{
    private readonly UserManager<ApplicationUser> _userManager = userManager; 
    private readonly ILogger<UsersSeeder> _logger = logger; 

    public async Task SeedAsync()
    {
        string passwordUserBasic = "Test123!";

        var user1 = new ApplicationUser 
        { 
            UserName = "user1",
            Email = "user@demo.com",
            FirstName = "User",
            LastName = "Demo",
            EmailConfirmed = true
        };

        _logger.LogInformation("Started seeding 'users' collection.");

        var result1 = await _userManager.CreateAsync(user1, passwordUserBasic);

        if (result1.Succeeded)
        {
            await _userManager.AddToRoleAsync(user1, StellarRoles.Basic);
            _logger.LogInformation($"Added a user to the database with 'ID': {user1.Id}, and 'UserName': {user1.UserName}.");
        }  
         
        _logger.LogInformation("Finished seeding 'users' collection.");
    } 
}
