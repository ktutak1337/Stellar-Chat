using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StellarChat.Shared.Abstractions.API.Endpoints;

namespace StellarChat.Shared.Infrastructure.API.Endpoints;

public static class Extensions
{
    private static readonly List<IEndpoint> registeredEndpoints = new();

    public static IServiceCollection RegisterEndpoints(this IServiceCollection services, IConfiguration configuration)
    {
        var endpoints = DiscoverEndpoints();

        foreach (var endpoint in endpoints)
        {
            endpoint.Register(services, configuration);
            registeredEndpoints.Add(endpoint);
        }

        return services;
    }

    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
    {
        foreach (var endpoint in registeredEndpoints)
        {
            endpoint.Use(app);
        }

        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var endpoint in registeredEndpoints)
        {
            endpoint.Expose(app);
        }

        return app;
    }

    private static IList<IEndpoint> DiscoverEndpoints()
        => AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IEndpoint)))
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>()
            .ToList();
}
