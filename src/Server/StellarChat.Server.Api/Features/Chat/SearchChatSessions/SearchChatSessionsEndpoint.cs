namespace StellarChat.Server.Api.Features.Chat.SearchChatSessions;

internal sealed class SearchChatSessionsEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var chatHistory = endpoints.MapGroup("/chat-history").WithTags("Chat history");

        chatHistory.MapGet("/sessions/search", async ([AsParameters] SearchChatSessions query, IMediator mediator) => IEndpoint.Select(await mediator.Send(query)))
            .Produces<List<ChatSessionResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Searches for chat sessions based on the provided query.",
                Description = PaginationDescription
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }

    private const string PaginationDescription = $"The query parameter is used to search for chat sessions based on the content of their messages. " +
                          $"The `Page` and `PageSize` query parameters are used for pagination.\n" +
                          $"\nWhen page = 1 and pageSize = 0, the entire collection of matching results is returned. Otherwise " +
                          $"results follow the specified pagination parameters.";
}
