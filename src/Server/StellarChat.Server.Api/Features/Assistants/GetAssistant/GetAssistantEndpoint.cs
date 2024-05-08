namespace StellarChat.Server.Api.Features.Assistants.GetAssistant;

public class GetAssistantEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var assistants = endpoints.MapGroup("/assistants").WithTags("Assistants");

        assistants.MapGet("{id:guid}", async (Guid id, IMediator mediator) => IEndpoint.Select(await mediator.Send(new GetAssistant { Id = id })))
            .Produces<ChatSessionResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetAssistant")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Retrieves a single assistant by 'chatId'."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
