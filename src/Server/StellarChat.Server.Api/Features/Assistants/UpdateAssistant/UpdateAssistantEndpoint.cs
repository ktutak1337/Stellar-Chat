namespace StellarChat.Server.Api.Features.Assistants.UpdateAssistant;

internal sealed class UpdateAssistantEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var assistants = endpoints.MapGroup("/assistants").WithTags("Assistants");

        assistants.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateAssistantRequest request, IMediator mediator) =>
        {
            var assistant = new UpdateAssistant(
                id,
                request.Name,
                request.Metaprompt,
                request.Description,
                request.AvatarUrl,
                request.DefaultModel,
                request.DefaultVoice,
                request.IsDefault
                );

            await mediator.Send(assistant);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Updates the details of a specific assistant by its 'id'."
        });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
