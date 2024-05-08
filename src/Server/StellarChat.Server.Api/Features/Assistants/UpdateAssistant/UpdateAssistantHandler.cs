using StellarChat.Server.Api.Features.Assistants.CreateAssistant;

namespace StellarChat.Server.Api.Features.Assistants.UpdateAssistant;

internal sealed class UpdateAssistantHandler : ICommandHandler<UpdateAssistant>
{
    private readonly IAssistantRepository _assistantRepository;
    private readonly IDefaultAssistantService _defaultAssistantService;
    private readonly TimeProvider _clock;
    private readonly ILogger<CreateAssistantHandler> _logger;

    public UpdateAssistantHandler(IAssistantRepository assistantRepository, IDefaultAssistantService defaultAssistantService, TimeProvider clock, ILogger<CreateAssistantHandler> logger)
    {
        _assistantRepository = assistantRepository;
        _defaultAssistantService = defaultAssistantService;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(UpdateAssistant command, CancellationToken cancellationToken)
    {
        var (id, name, metaprompt, description, avatarUrl, defaultModel, defaultVoice, shouldBeDefault) = command;

        var assistantToUpdate = await _assistantRepository.GetAsync(id) ?? throw new AssistantNotFoundException(id);

        var now = _clock.GetUtcNow();

        var isCurrentlyNotDefault = !_defaultAssistantService.IsCurrentlyDefault(assistantToUpdate);
        var shouldBeSetAsDefault = shouldBeDefault && isCurrentlyNotDefault;

        if (shouldBeSetAsDefault)
        {
            await _defaultAssistantService.RevokeCurrentAsync();
            await _defaultAssistantService.SetAsDefaultAsync(assistantToUpdate, saveChanges: false);
        }

        assistantToUpdate.Name = name;
        assistantToUpdate.Metaprompt = metaprompt;
        assistantToUpdate.Description = description;
        assistantToUpdate.AvatarUrl = avatarUrl;
        assistantToUpdate.DefaultModel = defaultModel;
        assistantToUpdate.DefaultVoice = defaultVoice;
        assistantToUpdate.UpdatedAt = now;

        await _assistantRepository.UpdateAsync(assistantToUpdate);
        _logger.LogInformation($"Assistant with ID: '{id}' has been updated.");

        return Unit.Value;
    }
}
