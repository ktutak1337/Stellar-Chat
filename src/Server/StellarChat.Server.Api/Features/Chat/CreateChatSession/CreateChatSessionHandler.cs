namespace StellarChat.Server.Api.Features.Chat.CreateChatSession;

internal class CreateChatSessionHandler : ICommandHandler<CreateChatSession>
{
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<CreateChatSessionHandler> _logger;

    public CreateChatSessionHandler(IChatSessionRepository chatSessionRepository, TimeProvider clock, ILogger<CreateChatSessionHandler> logger)
    {
        _chatSessionRepository = chatSessionRepository;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(CreateChatSession command, CancellationToken cancellationToken)
    {
        if (await _chatSessionRepository.ExistsAsync(command.ChatId))
        {
            throw new ChatSessionAlreadyExistsException(command.ChatId);
        }

        var now = _clock.GetLocalNow();

        // TODO: Retrieve activePlugins and metaprompt from settings
        var activePlugins = new HashSet<string>();

        var chatSession = ChatSession.Create(command.ChatId, command.Title, metaprompt: "", activePlugins, command.AvatarUrl, createdAt: now, updatedAt: now);

        await _chatSessionRepository.AddAsync(chatSession);
        _logger.LogInformation($"Chat session with ID: '{chatSession.Id}' has been created.");

        // TODO: Create initial bot message 

        return Unit.Value;
    }
}
