
using MongoDB.Bson;

namespace StellarChat.Server.Api.Features.Chat.SearchChatSessions;

internal sealed class SearchChatSessionsHandler : IQueryHandler<SearchChatSessions, Paged<ChatSessionResponse>>
{
    private readonly IMongoRepository<ChatSessionDocument, Guid> _chatSessionRepository;
    private readonly IMongoRepository<ChatMessageDocument, Guid> _chatMessageRepository;
    private readonly IAssistantRepository _assistantRepository;

    public SearchChatSessionsHandler(IMongoRepository<ChatSessionDocument, Guid> chatSessionRepository, IMongoRepository<ChatMessageDocument, Guid> chatMessageRepository, IAssistantRepository assistantRepository)
    {
        _chatSessionRepository = chatSessionRepository;
        _chatMessageRepository = chatMessageRepository;
        _assistantRepository = assistantRepository;
    }

    public async ValueTask<Paged<ChatSessionResponse>> Handle(SearchChatSessions query, CancellationToken cancellationToken)
    {
        var regexPattern = new BsonRegularExpression($".*{query.Query}.*", "i");
        var filter = Builders<ChatMessageDocument>.Filter.Regex("Content", regexPattern);

        var chatMessageIds = await _chatMessageRepository.Collection
            .Find(filter)
            .Project(message => message.ChatId)
            .ToListAsync(cancellationToken);

        var distinctChatMessageIds = chatMessageIds.Distinct().ToList();

        var chatSessions = _chatSessionRepository
            .Collection
            .AsQueryable()
            .Where(session => distinctChatMessageIds.Contains(session.Id));

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

    private bool ShouldReturnAllResults(SearchChatSessions query) => (query.Page, query.PageSize) == (1, 0);

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
