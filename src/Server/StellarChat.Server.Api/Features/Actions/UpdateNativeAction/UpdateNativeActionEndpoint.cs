using Webhook = StellarChat.Server.Api.Domain.Actions.Models.Webhook;

namespace StellarChat.Server.Api.Features.Actions.UpdateNativeAction;

internal sealed class UpdateNativeActionEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var actions = endpoints.MapGroup("/actions").WithTags("Actions");

        actions.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateNativeActionRequest request, IMediator mediator) =>
        {
            var action = new UpdateNativeAction(
                id,
                request.Name,
                request.Category,
                request.Icon,
                request.Model,
                request.Metaprompt,
                request.IsSingleMessageMode,
                request.IsRemoteAction,
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

            await mediator.Send(action);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Updates the details of a specific action by its 'id'."
        });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
