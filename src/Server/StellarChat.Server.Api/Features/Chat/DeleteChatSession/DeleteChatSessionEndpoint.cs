namespace StellarChat.Server.Api.Features.Chat.DeleteChatSession;

internal sealed class DeleteChatSessionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chatHistory = endpoints.MapGroup("/chat-history").WithTags("Chat history");

        chatHistory.MapDelete("/sessions/{chatId:guid}", async (Guid chatId, IMediator mediator) =>
        {
            await mediator.Send(new DeleteChatSession(chatId));
            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Deletes a single chat session by 'chatId'."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
