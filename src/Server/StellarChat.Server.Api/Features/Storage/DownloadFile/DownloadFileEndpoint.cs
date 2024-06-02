using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Server.Api.Features.Storage.DownloadFile;

public class DownloadFileEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var storage = endpoints.MapGroup("/files").WithTags("Storage");
        storage.MapGet("{fileId}", async (string fileId, IMediator mediator) =>
        {
            var response = await mediator.Send(new DownloadFile(fileId));

            if (response.Stream is null)
            {
                return Results.NotFound();
            }

            return Results.File(response.Stream, response.ContentType, response.FileName);
        })
            .Produces<DownloadFileResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("DownloadFile")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Download a file by 'fileId'."
            });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
