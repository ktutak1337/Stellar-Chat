namespace StellarChat.Server.Api.Features.Settings.UpdateIntegrations;

public class UpdateIntegrationsEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var routes = endpoints.MapGroup("/settings").WithTags("Settings");

        routes.MapPut("/{key}/integrations", async (string key, [FromBody] UpdateIntegrationsRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<UpdateIntegrations>();
            command = command with { SettingsKey = key };

            await mediator.Send(command);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Updates the integrations section of the application settings by 'key'",
            Description = $"Modifies the integrations within the application settings, including name, endpoint, API key, and enabled status.\n" +
                          $"\nThe `key` is associated with the application settings and its value is always `app-settings`."
        });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
