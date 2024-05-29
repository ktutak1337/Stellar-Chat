namespace StellarChat.Server.Api.Features.Actions.BrowseNativeAction;

public class BrowseNativeActionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var actions = endpoints.MapGroup("/actions").WithTags("Actions");

        actions.MapGet("", async ([AsParameters] BrowseNativeActions query, IMediator mediator) => IEndpoint.Select(await mediator.Send(query)))
            .Produces<NativeActionResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Retrieves all actions."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
