namespace StellarChat.Server.Api.Features.Actions.CreateNativeAction;

internal sealed class CreateNativeActionHandler : ICommandHandler<CreateNativeAction>
{
    private readonly INativeActionRepository _nativeActionRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<CreateNativeAction> _logger;

    public CreateNativeActionHandler(INativeActionRepository nativeActionRepository, TimeProvider clock, ILogger<CreateNativeAction> logger)
    {
        _nativeActionRepository = nativeActionRepository;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(CreateNativeAction command, CancellationToken cancellationToken)
    {
        var (id, name, category, icon, model, metaprompt, isRemoteAction, shouldRephraseResponse, webhook) = command;

        if (await _nativeActionRepository.ExistsAsync(id))
        {
            throw new NativeActionAlreadyExistsException(id);
        }

        var now = _clock.GetLocalNow();

        var action = NativeAction.Create(
            id,
            name,
            category,
            icon,
            model,
            metaprompt,
            isRemoteAction,
            shouldRephraseResponse,
            webhook,
            createdAt: now,
            updatedAt: now);

        await _nativeActionRepository.AddAsync(action);
        _logger.LogInformation($"Action with ID: '{id}' has been created.");

        return Unit.Value;
    }
}
