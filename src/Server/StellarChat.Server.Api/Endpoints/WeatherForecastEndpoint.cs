using StellarChat.Shared.Abstractions.API.Endpoints;

namespace StellarChat.Server.Api.Endpoints;

internal sealed class WeatherForecastEndpoint : IEndpoint
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"

        };

        endpoints.MapGet("/weatherforecast", (ILogger<WeatherForecast> logger) =>
        {
            logger.LogInformation("test logginig");
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
