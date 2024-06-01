namespace StellarChat.Server.Api.Features.Settings.GetSettings;

public class GetSettingsEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var routes = endpoints.MapGroup("/settings").WithTags("Settings");

        routes.MapGet("{key}", async (string key, IMediator mediator) => IEndpoint.Select(await mediator.Send(new GetSettings { Key = key })))
            .Produces<AppSettingsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetSettings")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Retrieves a single application settings by 'key'.",
                Description = "Fetches the application settings. The `key` for the settings is always `app-settings`."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
