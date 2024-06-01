namespace StellarChat.Server.Api.Features.Settings.UpdateProfile;

public class UpdateProfileEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var routes = endpoints.MapGroup("/settings").WithTags("Settings");

        routes.MapPut("/{key}/profile", async (string key, [FromBody] UpdateProfileRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<UpdateProfile>();
            await mediator.Send(command);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Updates the profile section of the application settings by 'key'",
            Description = $"Modifies the profile information within the application settings, including name, avatar URL, and description.\n" +
                          $"\nThe `key` is associated with the application settings and its value is always `app-settings`."
        });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
