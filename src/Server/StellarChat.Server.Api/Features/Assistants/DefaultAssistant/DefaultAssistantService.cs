namespace StellarChat.Server.Api.Features.Assistants.DefaultAssistant;

internal sealed class DefaultAssistantService : IDefaultAssistantService
{
    private readonly IAssistantRepository _assistantRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<DefaultAssistantService> _logger;

    public DefaultAssistantService(IAssistantRepository assistantRepository, TimeProvider clock, ILogger<DefaultAssistantService> logger)
    {
        _assistantRepository = assistantRepository;
        _clock = clock;
        _logger = logger;
    }

    public bool IsCurrentlyDefault(Assistant assistant)
        => assistant.IsDefault;

    public async ValueTask SetAsDefaultAsync(Assistant assistant, bool saveChanges = true)
    {
        if (assistant is not null)
        {
            var now = _clock.GetUtcNow();

            assistant.IsDefault = true;
            assistant.UpdatedAt = now;

            if (!saveChanges)
            {
                _logger.LogInformation($"Default assistant prepared to be set to new assistant with ID: {assistant.Id}, pending save.");
                return;
            }

            await _assistantRepository.UpdateAsync(assistant);
            _logger.LogInformation($"Default assistant has been updated to new assistant with ID: {assistant.Id}");
        }
    }

    public async ValueTask RevokeCurrentAsync()
    {
        var assistants = await _assistantRepository.BrowseAsync();
        var currentDefaultAssistant = assistants.SingleOrDefault(assistant => assistant.IsDefault);

        if (currentDefaultAssistant is not null)
        {
            var now = _clock.GetUtcNow();

            currentDefaultAssistant.IsDefault = false;
            currentDefaultAssistant.UpdatedAt= now;

            await _assistantRepository.UpdateAsync(currentDefaultAssistant);
            _logger.LogInformation($"Previous default assistant with ID: {currentDefaultAssistant.Id} has been unset.");
        }
    }
}
