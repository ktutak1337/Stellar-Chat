namespace StellarChat.Server.Api.Features.Chat.GetChatSession;

internal sealed class GetChatSessionHandler : IQueryHandler<GetChatSession, ChatSessionResponse>
{
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly IAssistantRepository _assistantRepository;

    public GetChatSessionHandler(IChatSessionRepository chatSessionRepository, IAssistantRepository assistantRepository)
    {
        _chatSessionRepository = chatSessionRepository;
        _assistantRepository = assistantRepository;
    }

    public async ValueTask<ChatSessionResponse> Handle(GetChatSession query, CancellationToken cancellationToken)
    {
        var chatSession = (await _chatSessionRepository.GetAsync(query.Id))
            .Adapt<ChatSessionResponse>() ?? throw new ChatSessionNotFoundException(query.Id);
        
        var assistant = (await _assistantRepository.GetAsync(chatSession.AssistantId))
            .Adapt<AssistantResponse>() ?? throw new AssistantNotFoundException(chatSession.AssistantId);
        
        var response = chatSession with { AssignedAssistant = assistant };
        return response;
    }
}
