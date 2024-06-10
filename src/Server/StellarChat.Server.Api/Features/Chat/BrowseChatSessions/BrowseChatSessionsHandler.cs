namespace StellarChat.Server.Api.Features.Chat.BrowseChatSessions;

internal sealed class BrowseChatSessionsHandler : IQueryHandler<BrowseChatSessions, Paged<ChatSessionResponse>>
{
    private readonly IMongoRepository<ChatSessionDocument, Guid> _chatSessionRepository;
    private readonly IAssistantRepository _assistantRepository;

    public BrowseChatSessionsHandler(IMongoRepository<ChatSessionDocument, Guid> chatSessionRepository, IAssistantRepository assistantRepository)
    {
        _chatSessionRepository = chatSessionRepository;
        _assistantRepository = assistantRepository;
    }

    public async ValueTask<Paged<ChatSessionResponse>> Handle(BrowseChatSessions query, CancellationToken cancellationToken)
    {
        var chatSessions = _chatSessionRepository.Collection?.AsQueryable();
        var assistants = await _assistantRepository.BrowseAsync();

        if (ShouldReturnAllResults(query))
        {
            var count = chatSessions!.Count();
            var chatSessionResponses = chatSessions.Adapt<IReadOnlyList<ChatSessionResponse>>().ToList();
            var updatedChatSessionResponses = AssignAssistantsToChatSessions(chatSessionResponses, assistants);

            return new()
            {
                CurrentPage = 1,
                TotalPages = 1,
                TotalResults = count,
                ResultsPerPage = count,
                Items = updatedChatSessionResponses
            };
        }

        var result = await chatSessions!.PaginateAsync(query);
        var chatSessionResponsesPaginated = result.Items.Adapt<List<ChatSessionResponse>>();
        var updatedChatSessionResponsesPaginated = AssignAssistantsToChatSessions(chatSessionResponsesPaginated, assistants);

        return new()
        {
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalResults = result.TotalResults,
            ResultsPerPage = result.ResultsPerPage,
            Items = updatedChatSessionResponsesPaginated
        };
    }

    private bool ShouldReturnAllResults(BrowseChatSessions query) => (query.Page, query.PageSize) == (1, 0);

    private List<ChatSessionResponse> AssignAssistantsToChatSessions(List<ChatSessionResponse> chatSessionResponses, IEnumerable<Assistant> assistants)
    {
        return chatSessionResponses.Select(chatSessionResponse =>
        {
            var assistant = assistants.SingleOrDefault(a => a.Id == chatSessionResponse.AssistantId)
                ?.Adapt<AssistantResponse>();
            
            return chatSessionResponse with { AssignedAssistant = assistant! };
        }).ToList();
    }
}





