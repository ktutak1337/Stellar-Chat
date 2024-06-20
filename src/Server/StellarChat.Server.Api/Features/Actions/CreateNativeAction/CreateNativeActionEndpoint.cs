using Webhook = StellarChat.Server.Api.Domain.Actions.Models.Webhook;

namespace StellarChat.Server.Api.Features.Actions.CreateNativeAction;

internal sealed class CreateNativeActionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var actions = endpoints.MapGroup("/actions").WithTags("Actions");

        actions.MapPost("", async ([FromBody] CreateNativeActionRequest request, IMediator mediator) =>
        {
            var id = Guid.NewGuid();

            var command = new CreateNativeAction(
                id,
                request.Name,
                request.Category,
                request.Icon,
                request.Model,
                request.Metaprompt,
                request.IsRemoteAction,
                request.IsSingleMessageMode,
                request.ShouldRephraseResponse,
                Webhook.Create(
                    request.Webhook!.httpMethod,
                    request.Webhook.url,
                    request.Webhook.payload,
                    request.Webhook.isRetryEnabled,
                    request.Webhook.retryCount,
                    request.Webhook.retryInterval,
                    request.Webhook.isScheduled,
                    request.Webhook.cronExpression,
                    request.Webhook.headers));

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
