using StellarChat.Server.Api.Features.Chat.CreateChatSession;

namespace StellarChat.Server.Api.Features.Assistants.CreateAssistant;

internal sealed class CreateAssistantEndpoint : IEndpoint 
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var assistants = endpoints.MapGroup("/assistants").WithTags("Assistants");

        assistants.MapPost("", async ([FromBody] CreateAssistantRequest request, IMediator mediator) =>
        {
            var id = Guid.NewGuid();
            var command = request.Adapt<CreateAssistant>();

            command = command with { Id = id };
            await mediator.Send(command);

            return Results.CreatedAtRoute("GetAssistant", new { Id = id }, id);
        })
         .Produces(StatusCodes.Status201Created)
         .Produces(StatusCodes.Status400BadRequest)
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Creates a new assistant."
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
