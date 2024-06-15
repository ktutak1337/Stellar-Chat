using DocumentFormat.OpenXml.Spreadsheet;
using StackExchange.Redis;
using StellarChat.Shared.Infrastructure.DAL.Mongo;

namespace StellarChat.Server.Api.Features.Assistants.BrowseAssistants;

public class BrowseAssistantsEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var assistants = endpoints.MapGroup("/assistants").WithTags("Assistants");

        assistants.MapGet("", async ([AsParameters] BrowseAssistants query, IMediator mediator) => IEndpoint.Select(await mediator.Send(query)))
            .Produces<List<AssistantResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Retrieves all assistants.",
                Description = PaginationDescription
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }

    private const string PaginationDescription = $"The `Page` and `PageSize` query parameters are used for pagination.\n" +
                          $"\nWhen page = 1 and pageSize = 0, the entire collection is returned.Otherwise, " +
                          $"results follow the specified pagination parameters.";
}
