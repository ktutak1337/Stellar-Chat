namespace StellarChat.Server.Api.Features.Chat.BrowseChatSessions;

internal sealed class BrowseChatSessionsEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chatHistory = endpoints.MapGroup("/chat-history").WithTags("Chat history");

        chatHistory.MapGet("/sessions", async ([AsParameters] BrowseChatSessions query, IMediator mediator) => IEndpoint.Select(await mediator.Send(query)))
            .Produces<List<ChatSessionResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Retrieves all chat sessions.",
                Description = PaginationDescription
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }

    private const string PaginationDescription = $"The `Page` and `PageSize` query parameters are used for pagination.\n" +
                          $"\nWhen page = 1 and pageSize = 0, the entire collection is returned.Otherwise, " +
                          $"results follow the specified pagination parameters.";
}
