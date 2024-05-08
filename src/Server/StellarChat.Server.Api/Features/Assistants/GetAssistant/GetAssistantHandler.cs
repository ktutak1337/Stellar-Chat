namespace StellarChat.Server.Api.Features.Assistants.GetAssistant;

internal sealed class GetAssistantHandler : IQueryHandler<GetAssistant, AssistantResponse>
{
    private readonly IAssistantRepository _assistantRepository;

    public GetAssistantHandler(IAssistantRepository assistantRepository)
        => _assistantRepository = assistantRepository;

    public async ValueTask<AssistantResponse> Handle(GetAssistant query, CancellationToken cancellationToken)
        => (await _assistantRepository.GetAsync(query.Id))
            .Adapt<AssistantResponse>() ?? throw new AssistantNotFoundException(query.Id);
}
