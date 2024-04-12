namespace StellarChat.Server.Api.Features.Chat.CreateChatSession;

internal sealed class CreateChatSessionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chatHistory = endpoints.MapGroup("/chat-history").WithTags("Chat history");

        chatHistory.MapPost("/sessions", async ([FromBody] CreateChatSessionRequest request, IMediator mediator) =>
        {
            var chatId = Guid.NewGuid();
            var command = request.Adapt<CreateChatSession>();

            command = command with { ChatId = chatId };
            await mediator.Send(command);

            return Results.CreatedAtRoute("GetChatSession", new { ChatId = chatId }, chatId);
        })
         .Produces(StatusCodes.Status201Created)
         .Produces(StatusCodes.Status400BadRequest)
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Creates a new chat session."
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
