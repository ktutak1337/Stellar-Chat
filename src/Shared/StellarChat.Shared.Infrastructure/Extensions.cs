using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace StellarChat.Shared.Infrastructure;

public static class Extensions
{
    private const string CorrelationIdKey = "correlation-id";

    public static string ToSnakeCase(this string input)
        => Regex.Replace(
            Regex.Replace(
                Regex.Replace(input, @"([\p{Lu}]+)([\p{Lu}][\p{Ll}])", "$1_$2"), @"([\p{Ll}\d])([\p{Lu}])", "$1_$2"), @"[-\s]", "_").ToLower();

    public static bool IsEmpty(this string value) 
        => string.IsNullOrWhiteSpace(value);

    public static bool IsNotEmpty(this string value)
        => !value.IsEmpty();

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    => app.Use((ctx, next) =>
    {
        ctx.Items.Add(CorrelationIdKey, Guid.NewGuid());
        return next();
    });

    public static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => BindOptions<T>(configuration.GetSection(sectionName));

    public static T BindOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }

    public static Guid? TryGetCorrelationId(this HttpContext context)
        => context.Items.TryGetValue(CorrelationIdKey, out var id) ? (Guid?)id : null;

    public static string GetUserIpAddress(this HttpContext context)
    {
        if (context is null)
        {
            return string.Empty;
        }

        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        if (context.Request.Headers.TryGetValue("x-forwarded-for", out var forwardedFor))
        {
            var ipAddresses = forwardedFor.ToString().Split(",", StringSplitOptions.RemoveEmptyEntries);
            
            if (ipAddresses.Any())
            {
                ipAddress = ipAddresses[0];
            }
        }

        return ipAddress ?? string.Empty;
    }
}
