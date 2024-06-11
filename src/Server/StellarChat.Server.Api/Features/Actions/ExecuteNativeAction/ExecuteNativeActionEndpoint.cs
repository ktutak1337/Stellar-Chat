namespace StellarChat.Server.Api.Features.Actions.ExecuteNativeAction;

public class ExecuteNativeActionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var actions = endpoints.MapGroup("/actions").WithTags("Actions");

        actions.MapPost("execute", async ([FromBody] ExecuteNativeActionRequest request, IMediator mediator) =>
        {
            var id = Guid.NewGuid();
            var command = request.Adapt<ExecuteNativeAction>();

            command = command with { Id = id };
            var result = await mediator.Send(command);

            return Results.Ok(result);
        })
         .Produces(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status400BadRequest)
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Executes a new action."
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
