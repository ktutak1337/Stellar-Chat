namespace StellarChat.Server.Api.Features.Chat.ChangeChatSessionTitle;

internal sealed class ChangeChatSessionTitleHandler : ICommandHandler<ChangeChatSessionTitle>
{
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<ChangeChatSessionTitleHandler> _logger;

    public ChangeChatSessionTitleHandler(IChatSessionRepository chatSessionRepository, TimeProvider clock,
        ILogger<ChangeChatSessionTitleHandler> logger)
    {
        _chatSessionRepository = chatSessionRepository;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(ChangeChatSessionTitle command, CancellationToken cancellationToken)
    {
        var (chatId, title) = command;

        var chatSession = await _chatSessionRepository.GetAsync(chatId) ?? throw new ChatSessionNotFoundException(chatId);

        var now = _clock.GetUtcNow();

        chatSession.Title = title;
        chatSession.UpdatedAt = now;

        await _chatSessionRepository.UpdateAsync(chatSession);
        _logger.LogInformation($"Title for chat session with ID: '{chatId}' has been updated.");

        return Unit.Value;
    }
}
