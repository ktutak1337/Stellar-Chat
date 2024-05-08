namespace StellarChat.Server.Api.Features.Assistants.DeleteAssistant;

internal sealed class DeleteAssistantHandler : ICommandHandler<DeleteAssistant>
{
    private readonly IAssistantRepository _assistantRepository;
    private readonly IDefaultAssistantService _defaultAssistantService;
    private readonly ILogger<DeleteAssistantHandler> _logger;

    public DeleteAssistantHandler(
        IAssistantRepository assistantRepository, IDefaultAssistantService defaultAssistantService, ILogger<DeleteAssistantHandler> logger)
    {
        _assistantRepository = assistantRepository;
        _defaultAssistantService = defaultAssistantService;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(DeleteAssistant command, CancellationToken cancellationToken)
    {
        var id = command.Id;
        
        var assistantToDelete = await _assistantRepository.GetAsync(id) ?? throw new AssistantNotFoundException(id);
        var assistants = await _assistantRepository.BrowseAsync();

        if (assistants.Count() <= 1)
        {
            throw new CannotDeleteLastAssistantException(id);
        }
        
        if (assistantToDelete.IsDefault)
        {
            await _defaultAssistantService.RevokeCurrentAsync();
            await AssignNewDefaultAsync(assistants, id);
        }

        await _assistantRepository.DeleteAsync(id);

        _logger.LogInformation($"Assistant with ID: '{id}' has been deleted.");

        return Unit.Value;
    }

    private async Task AssignNewDefaultAsync(IEnumerable<Assistant> assistants, Guid excludedAssistantId)
    {
        var newDefaultAssistant = assistants.FirstOrDefault(assistant => assistant.Id != excludedAssistantId);

        if (newDefaultAssistant is not null)
        {
            newDefaultAssistant.IsDefault = true;
            await _assistantRepository.UpdateAsync(newDefaultAssistant);
            _logger.LogInformation($"New default assistant assigned with ID: '{newDefaultAssistant.Id}'.");
        }
    }
}
