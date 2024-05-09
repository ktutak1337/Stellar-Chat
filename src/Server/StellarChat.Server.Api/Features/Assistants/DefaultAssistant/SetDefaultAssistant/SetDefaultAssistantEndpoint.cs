namespace StellarChat.Server.Api.Features.Assistants.DefaultAssistant.SetDefaultAssistant;

public class SetDefaultAssistantEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var assistants = endpoints.MapGroup("/assistants").WithTags("Assistants");

        assistants.MapPut("{id:guid}/default", async (Guid id, [FromBody] SetDefaultAssistantRequest request, IMediator mediator) =>
        {
            await mediator.Send(new SetDefaultAssistant(id, request.IsDefault));
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Sets the default assistant using its 'id'."
        });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}

