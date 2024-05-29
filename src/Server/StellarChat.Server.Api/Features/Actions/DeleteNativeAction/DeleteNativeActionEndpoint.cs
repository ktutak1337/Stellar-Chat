namespace StellarChat.Server.Api.Features.Actions.DeleteNativeAction;

internal sealed class DeleteNativeActionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var actions = endpoints.MapGroup("/actions").WithTags("Actions");

        actions.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteNativeAction(id));
            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Deletes a single action by 'id'."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}