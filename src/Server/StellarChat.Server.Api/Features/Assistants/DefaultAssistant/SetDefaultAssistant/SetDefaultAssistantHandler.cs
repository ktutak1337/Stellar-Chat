namespace StellarChat.Server.Api.Features.Assistants.DefaultAssistant.SetDefaultAssistant;

internal sealed class SetDefaultAssistantHandler : ICommandHandler<SetDefaultAssistant>
{
    private readonly IAssistantRepository _assistantRepository;
    private readonly IDefaultAssistantService _defaultAssistantService;

    public SetDefaultAssistantHandler(IAssistantRepository assistantRepository, IDefaultAssistantService defaultAssistantService)
    {
        _assistantRepository = assistantRepository;
        _defaultAssistantService = defaultAssistantService;
    }

    public async ValueTask<Unit> Handle(SetDefaultAssistant command, CancellationToken cancellationToken)
    {
        var (id, shouldBeDefault) = command;

        var assistant = await _assistantRepository.GetAsync(id) ?? throw new AssistantNotFoundException(id);

        var isCurrentlyNotDefault = !_defaultAssistantService.IsCurrentlyDefault(assistant);
        var shouldBeSetAsDefault = shouldBeDefault && isCurrentlyNotDefault;

        if (shouldBeSetAsDefault)
        {
            await _defaultAssistantService.RevokeCurrentAsync();
            await _defaultAssistantService.SetAsDefaultAsync(assistant, saveChanges: true);
        }

        return Unit.Value;
    }
}
