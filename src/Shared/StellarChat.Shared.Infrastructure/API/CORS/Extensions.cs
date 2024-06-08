using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace StellarChat.Shared.Infrastructure.API.CORS;
public static class Extensions
{
    private const string SectionName = "cors";
    private const string PolicyName = "cors";

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<CorsOptions>(section);
        var options = section.BindOptions<CorsOptions>();

        if (!options.ENABLED)
        {
            return services;
        }

        return services.AddCors(cors =>
        {
            var allowedHeaders = options.ALLOWED_HEADERS;
            var allowedMethods = options.ALLOWED_METHODS;
            var allowedOrigins = options.ALLOWED_ORIGINS;
            var exposedHeaders = options.ALLOWED_HEADERS;
            cors.AddPolicy(PolicyName, corsBuilder =>
            {
                var origins = allowedOrigins?.ToArray() ?? [];
                if (options.ALLOW_CREDENTIALS && origins.FirstOrDefault() != "*")
                {
                    corsBuilder.AllowCredentials();
                }
                else
                {
                    corsBuilder.DisallowCredentials();
                }
                corsBuilder
                    .WithHeaders(allowedHeaders?.ToArray() ?? [])
                    .WithMethods(allowedMethods?.ToArray() ?? [])
                    .WithOrigins(origins.ToArray())
                    .WithExposedHeaders(exposedHeaders?.ToArray() ?? []);
            });
        });
    }

    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
    {
        var options = app.ApplicationServices.GetRequiredService<IOptions<CorsOptions>>().Value;
        if (!options.ENABLED)
        {
            return app;
        }

        app.UseCors(PolicyName);

        return app;
    }
}
