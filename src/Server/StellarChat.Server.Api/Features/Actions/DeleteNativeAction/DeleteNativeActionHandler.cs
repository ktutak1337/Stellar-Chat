namespace StellarChat.Server.Api.Features.Actions.DeleteNativeAction;

internal sealed class DeleteNativeActionHandler : ICommandHandler<DeleteNativeAction>
{
    private readonly INativeActionRepository _nativeActionRepository;
    private readonly ILogger<DeleteNativeActionHandler> _logger;

    public DeleteNativeActionHandler(INativeActionRepository nativeActionRepository, ILogger<DeleteNativeActionHandler> logger)
    {
        _nativeActionRepository = nativeActionRepository;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(DeleteNativeAction command, CancellationToken cancellationToken)
    {
        if (!await _nativeActionRepository.ExistsAsync(command.Id))
        {
            throw new NativeActionNotFoundException(command.Id);
        }

        await _nativeActionRepository.DeleteAsync(command.Id);
        _logger.LogInformation($"Action with ID: '{command.Id}' has been deleted.");

        return Unit.Value;
    }
}
