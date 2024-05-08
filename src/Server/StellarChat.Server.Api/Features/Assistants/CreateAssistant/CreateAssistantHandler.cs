namespace StellarChat.Server.Api.Features.Assistants.CreateAssistant;

internal sealed class CreateAssistantHandler : ICommandHandler<CreateAssistant>
{
    private readonly IAssistantRepository _assistantRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<CreateAssistantHandler> _logger;

    public CreateAssistantHandler(IAssistantRepository assistantRepository, TimeProvider clock, ILogger<CreateAssistantHandler> logger)
    {
        _assistantRepository = assistantRepository;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(CreateAssistant command, CancellationToken cancellationToken)
    {
        if (await _assistantRepository.ExistsAsync(command.Id))
        {
            throw new AssistantAlreadyExistsException(command.Id);
        }
        
        var now = _clock.GetUtcNow();

        if (command.IsDefault)
        {
            await UnsetPreviousDefaultAssistant();
        }
        
        var assistant = Assistant.Create(
            command.Id,
            command.Name,
            command.Metaprompt,
            command.Description,
            command.AvatarUrl,
            command.DefaultModel,
            command.DefaultVoice,
            command.IsDefault,
            createdAt: now,
            updatedAt: now);

        await _assistantRepository.AddAsync(assistant);
        _logger.LogInformation($"Assistant with ID: '{assistant.Id}' has been created.");

        return Unit.Value;
    }

    private async Task UnsetPreviousDefaultAssistant()
    {
        var assistants = await _assistantRepository.BrowseAsync();
        var currentDefaultAssistant = assistants.SingleOrDefault(assistant => assistant.IsDefault);
        
        if (currentDefaultAssistant is not null)
        {
            currentDefaultAssistant.IsDefault = false;
            await _assistantRepository.UpdateAsync(currentDefaultAssistant);
        }
    }
}

