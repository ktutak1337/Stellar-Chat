namespace StellarChat.Server.Api.Features.Chat.GetChatMessagesByChatId;

public class GetChatMessagesByChatIdEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chatHistory = endpoints.MapGroup("/chat-history").WithTags("Chat history");

        chatHistory.MapGet("/sessions/{chatId:guid}/messages", async ([AsParameters] GetChatMessagesByChatId query, IMediator mediator)
            => IEndpoint.Select(await mediator.Send(query)))
                .Produces<List<ChatMessageResponse>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Retrieves chat messages for a specific chat session by 'chatId'.",
                    Description = PaginationDescription
                });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }

    private const string PaginationDescription = $"The `Page` and `PageSize` query parameters are used for pagination.\n" +
                              $"\nWhen the `Page` and `PageSize` are set to `0` the entire collection will be returned; " +
                              $"otherwise, results will be returned based on the specified pagination parameters.";
}
