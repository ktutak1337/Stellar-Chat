using Mapster;
using Mediator;
using MongoDB.Driver;
using StellarChat.Server.Api.DAL.Mongo.Documents.Chat;
using StellarChat.Shared.Abstractions.Contracts.Chat;
using StellarChat.Shared.Abstractions.Pagination;
using StellarChat.Shared.Infrastructure.DAL.Mongo;

namespace StellarChat.Server.Api.Features.Chat.BrowseChatSessions;

internal sealed class BrowseChatSessionsHandler : IQueryHandler<BrowseChatSessions, Paged<ChatSessionResponse>>
{
    private readonly IMongoRepository<ChatSessionDocument, Guid> _chatSessionRepository;

    public BrowseChatSessionsHandler(IMongoRepository<ChatSessionDocument, Guid> chatSessionRepository) 
        => _chatSessionRepository = chatSessionRepository;

    public async ValueTask<Paged<ChatSessionResponse>> Handle(BrowseChatSessions query, CancellationToken cancellationToken)
    {
        var chatSessions = _chatSessionRepository.Collection?.AsQueryable();

        if (ShouldReturnAllResults(query))
        {
            var count = chatSessions!.Count();
            
            return new()
            {
                CurrentPage = 1,
                TotalPages = 1,
                TotalResults = count,
                ResultsPerPage = count,
                Items = chatSessions.Adapt<IReadOnlyList<ChatSessionResponse>>().ToList()
            };
        }

        var result = await chatSessions!.PaginateAsync(query);

        return new()
        {
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalResults = result.TotalResults,
            ResultsPerPage = result.ResultsPerPage,
            Items = result.Items.Select(document => document.Adapt<ChatSessionResponse>()).ToList()
        };
    }

    private bool ShouldReturnAllResults(BrowseChatSessions query) => (query.Page, query.PageSize) == (1, 0);
}
