using Mapster;
using MapsterMapper;
using System.Reflection;
using StellarChat.Shared.Infrastructure;

namespace StellarChat.Server.Api;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddSharedInfrastructure();

        builder.Services.AddMappings();
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
        => app.UseSharedInfrastructure();

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
