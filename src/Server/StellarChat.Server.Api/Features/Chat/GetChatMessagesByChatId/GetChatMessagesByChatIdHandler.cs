namespace StellarChat.Server.Api.Features.Chat.GetChatMessagesByChatId;

internal class GetChatMessagesByChatIdHandler : IQueryHandler<GetChatMessagesByChatId, Paged<ChatMessageResponse>>
{
    private readonly IMongoRepository<ChatMessageDocument, Guid> _chatMessageRepository;

    public GetChatMessagesByChatIdHandler(IMongoRepository<ChatMessageDocument, Guid> chatMessageRepository)
        => _chatMessageRepository = chatMessageRepository;

    public async ValueTask<Paged<ChatMessageResponse>> Handle(GetChatMessagesByChatId query, CancellationToken cancellationToken)
    {
        var chatSessions = _chatMessageRepository.Collection?
            .AsQueryable()
            .Where(document => document.ChatId == query.ChatId);

        if (ShouldReturnAllResults(query))
        {
            var count = chatSessions!.Count();
            return new()
            {
                CurrentPage = 1,
                TotalPages = 1,
                TotalResults = count,
                ResultsPerPage = count,
                Items = chatSessions.Adapt<IReadOnlyList<ChatMessageResponse>>().ToList()
            };
        }

        var result = await chatSessions!.PaginateAsync(query);

        return new()
        {
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalResults = result.TotalResults,
            ResultsPerPage = result.ResultsPerPage,
            Items = result.Items.Select(document => document.Adapt<ChatMessageResponse>()).ToList()
        };
    }

    private bool ShouldReturnAllResults(GetChatMessagesByChatId query) => (query.Page, query.PageSize) == (1, 0);
}
