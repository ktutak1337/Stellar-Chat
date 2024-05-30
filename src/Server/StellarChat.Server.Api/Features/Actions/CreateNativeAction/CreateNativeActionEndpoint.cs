namespace StellarChat.Server.Api.Features.Actions.CreateNativeAction;

internal sealed class CreateNativeActionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var actions = endpoints.MapGroup("/actions").WithTags("Actions");

        actions.MapPost("", async ([FromBody] CreateNativeActionRequest request, IMediator mediator) =>
        {
            var id = Guid.NewGuid();
            var command = request.Adapt<CreateNativeAction>();

            command = command with { Id = id };
            await mediator.Send(command);

            return Results.CreatedAtRoute("GetNativeAction", new { Id = id }, id);
        })
         .Produces(StatusCodes.Status201Created)
         .Produces(StatusCodes.Status400BadRequest)
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Creates a new action."
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
