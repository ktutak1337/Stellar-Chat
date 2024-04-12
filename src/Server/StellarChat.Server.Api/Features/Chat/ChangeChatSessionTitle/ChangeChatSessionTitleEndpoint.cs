namespace StellarChat.Server.Api.Features.Chat.ChangeChatSessionTitle;

internal sealed class ChangeChatSessionTitleEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chatHistory = endpoints.MapGroup("/chat-history").WithTags("Chat history");

        chatHistory.MapPut("/sessions/{chatId:guid}", async (Guid chatId, [FromBody] ChangeChatSessionTitleRequest request, IMediator mediator) =>
        {
            await mediator.Send(new ChangeChatSessionTitle(chatId, request.Title));
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Updates the title of a specific chat session using its 'chatId'."
        });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
