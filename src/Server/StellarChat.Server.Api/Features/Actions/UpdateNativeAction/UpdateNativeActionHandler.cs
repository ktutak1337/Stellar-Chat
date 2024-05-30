namespace StellarChat.Server.Api.Features.Actions.UpdateNativeAction;

internal sealed class UpdateNativeActionHandler : ICommandHandler<UpdateNativeAction>
{
    private readonly INativeActionRepository _nativeActionRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<UpdateNativeActionHandler> _logger;

    public UpdateNativeActionHandler(INativeActionRepository nativeActionRepository, TimeProvider clock, ILogger<UpdateNativeActionHandler> logger)
    {
        _nativeActionRepository = nativeActionRepository;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(UpdateNativeAction command, CancellationToken cancellationToken)
    {
        var (id, name, category, icon, model, metaprompt, isRemoteAction, shouldRephraseResponse, webhook) = command;

        var action = await _nativeActionRepository.GetAsync(id) ?? throw new NativeActionNotFoundException(id);

        var now = _clock.GetLocalNow();

        action.Name = name;
        action.Category = category;
        action.Icon = icon;
        action.Model = model;
        action.Metaprompt = metaprompt;
        action.IsRemoteAction = isRemoteAction;
        action.ShouldRephraseResponse = shouldRephraseResponse;
        
        if(isRemoteAction && action.Webhook is not null)
        {
            action.Webhook.HttpMethod = webhook!.HttpMethod;
            action.Webhook.Url = webhook.Url;
            action.Webhook.Payload = webhook.Payload;
            action.Webhook.IsRetryEnabled = webhook.IsRetryEnabled;
            action.Webhook.RetryCount = webhook.RetryCount;
            action.Webhook.RetryInterval = webhook.RetryInterval;
            action.Webhook.IsScheduled = webhook.IsScheduled;
            action.Webhook.CronExpression = webhook.CronExpression;
            action.Webhook.Headers = webhook.Headers;
        }

        action.UpdatedAt = now;

        await _nativeActionRepository.UpdateAsync(action);
        _logger.LogInformation($"Action with ID: '{id}' has been updated.");

        return Unit.Value;
    }
}
