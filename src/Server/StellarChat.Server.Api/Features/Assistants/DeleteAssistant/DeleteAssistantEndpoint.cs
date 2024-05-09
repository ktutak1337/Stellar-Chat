namespace StellarChat.Server.Api.Features.Assistants.DeleteAssistant;

internal sealed class DeleteAssistantEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var assistants = endpoints.MapGroup("/assistants").WithTags("Assistants");

        assistants.MapDelete("{id:guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteAssistant(id));
            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Deletes a single Assistant by 'id'.",
                Description = "If the deleted Assistant is the default, a new default will be assigned automatically. Cannot delete if only one Assistant exists."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
