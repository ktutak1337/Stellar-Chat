namespace StellarChat.Server.Api.Features.Chat.GetChatSession;

internal sealed class GetChatSessionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chatHistory = endpoints.MapGroup("/chat-history").WithTags("Chat history");

        chatHistory.MapGet("/sessions/{chatId:guid}", async (Guid chatId, IMediator mediator) => IEndpoint.Select(await mediator.Send(new GetChatSession { Id = chatId })))
            .Produces<ChatSessionResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetChatSession")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Retrieves a single chat session by 'chatId'."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
