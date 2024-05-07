namespace StellarChat.Server.Api.Features.Assistants.BrowseAssistants;

internal class BrowseAssistantsHandler : IQueryHandler<BrowseAssistants, Paged<AssistantResponse>>
{
    private readonly IMongoRepository<AssistantDocument, Guid> _assistantRepository;

    public BrowseAssistantsHandler(IMongoRepository<AssistantDocument, Guid> assistantRepository) 
        => _assistantRepository = assistantRepository;

    public async ValueTask<Paged<AssistantResponse>> Handle(BrowseAssistants query, CancellationToken cancellationToken)
    {
        var asistants = _assistantRepository.Collection?.AsQueryable();

        if (ShouldReturnAllResults(query))
        {
            var count = asistants!.Count();

            return new()
            {
                CurrentPage = 1,
                TotalPages = 1,
                TotalResults = count,
                ResultsPerPage = count,
                Items = asistants.Adapt<IReadOnlyList<AssistantResponse>>().ToList()
            };
        }

        var result = await asistants!.PaginateAsync(query);

        return new()
        {
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalResults = result.TotalResults,
            ResultsPerPage = result.ResultsPerPage,
            Items = result.Items.Select(document => document.Adapt<AssistantResponse>()).ToList()
        };
    }

    private bool ShouldReturnAllResults(BrowseAssistants query) => (query.Page, query.PageSize) == (1, 0);
}
