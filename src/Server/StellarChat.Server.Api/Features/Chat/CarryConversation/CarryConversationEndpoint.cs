namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

public class CarryConversationEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chats = endpoints.MapGroup("/chats").WithTags("Chats");

        chats.MapPost("/{chatId:guid}/messages", async (Guid chatId,[FromBody] AskRequest request, IMediator mediator) =>
        {
            var id = Guid.NewGuid();
            var command = request.Adapt<Ask>();

            command = command with { ChatId = chatId };
            var result = await mediator.Send(command);

            return Results.Ok(result);
        })
         .Produces(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status400BadRequest)
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Invokes the chat plugin to get a response from the Assistant."
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}

