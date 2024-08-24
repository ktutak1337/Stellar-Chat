using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StellarChat.Shared.Infrastructure.DAL.Mongo;
using StellarChat.Shared.Infrastructure.Identity.Seeders;

namespace StellarChat.Shared.Infrastructure.Identity;

public static class Extensions
{  
    public static IServiceCollection AddMongoIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<MongoOptions>>().Value;

        if (options == null) 
            return services; 

        var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
        {
            MongoDbSettings = new MongoDbSettings
            { 
                ConnectionString = options.CONNECTION_STRING,
                DatabaseName = "StellarChat"
            },
            IdentityOptionsAction = options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.RequireUniqueEmail = true;
            }
        };

        services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddDefaultTokenProviders();

        services.AddTransient<IMongoIdentitySeeder, MongoIdentitySeeder>();

        return services;
    }

    public static async Task SeedMongoIdentityAsync(this IServiceProvider services)
    { 
        using var scope = services.CreateScope(); 
        await scope.ServiceProvider.GetRequiredService<IMongoIdentitySeeder>()
            .SeedIdentityAsync();
    }
}
