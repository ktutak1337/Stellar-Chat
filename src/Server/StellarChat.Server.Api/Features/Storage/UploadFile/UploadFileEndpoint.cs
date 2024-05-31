using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Server.Api.Features.Storage.UploadFile;

public class UploadFileEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var storage = endpoints.MapGroup("/storage").WithTags("Storage");

        storage.MapPost("/upload", async ([FromForm] UploadFileRequest request, IMediator mediator) =>
        {
            var command = new UploadFile(request.File, request.Directory, request.Prefix);

            var result = await mediator.Send(command);

            return Results.Ok(result);
        })
         .DisableAntiforgery()
         .Produces(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status400BadRequest)
         .Produces(StatusCodes.Status500InternalServerError)
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Uploads a file.",
             Description = Description
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }

    private const string Description = $"`directory:` [**Optional**] The name of the directory where the file should be saved. " +
                      $"\nIf not provided by the user, the file type is recognized, and it is saved to the appropriate directories based on its type. \n" +
                      $"\n`prefix:` [**Optional**] The prefix to be added to the file name.If not provided, a unique identifier will be generated. " +
                      $"The file will be named using the format `_{{prefix}}_{{GUID}}`.";
}
