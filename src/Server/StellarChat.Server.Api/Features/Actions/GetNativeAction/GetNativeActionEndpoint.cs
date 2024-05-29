namespace StellarChat.Server.Api.Features.Actions.GetNativeAction;

public class GetNativeActionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var actions = endpoints.MapGroup("/actions").WithTags("Actions");

        actions.MapGet("/{id:guid}", async (Guid id, IMediator mediator) => IEndpoint.Select(await mediator.Send(new GetNativeAction { Id = id })))
            .Produces<NativeActionResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetNativeAction")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Retrieves a single action by 'id'."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
