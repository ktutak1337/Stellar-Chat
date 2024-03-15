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

        if (!options.Enabled)
        {
            return services;
        }

        return services.AddCors(cors =>
        {
            var allowedHeaders = options.AllowedHeaders;
            var allowedMethods = options.AllowedMethods;
            var allowedOrigins = options.AllowedOrigins;
            var exposedHeaders = options.ExposedHeaders;
            cors.AddPolicy(PolicyName, corsBuilder =>
            {
                var origins = allowedOrigins?.ToArray() ?? Array.Empty<string>();
                if (options.AllowCredentials && origins.FirstOrDefault() != "*")
                {
                    corsBuilder.AllowCredentials();
                }
                else
                {
                    corsBuilder.DisallowCredentials();
                }
                corsBuilder
                    .WithHeaders(allowedHeaders?.ToArray() ?? Array.Empty<string>())
                    .WithMethods(allowedMethods?.ToArray() ?? Array.Empty<string>())
                    .WithOrigins(origins.ToArray())
                    .WithExposedHeaders(exposedHeaders?.ToArray() ?? Array.Empty<string>());
            });
        });
    }

    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
    {
        var options = app.ApplicationServices.GetRequiredService<IOptions<CorsOptions>>().Value;
        if (!options.Enabled)
        {
            return app;
        }

        app.UseCors(PolicyName);

        return app;
    }
}
