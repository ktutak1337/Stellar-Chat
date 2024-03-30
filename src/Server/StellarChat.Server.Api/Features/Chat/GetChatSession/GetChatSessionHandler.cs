using Mapster;
using Mediator;
using StellarChat.Server.Api.DAL.Mongo.Exceptions.Chat;
using StellarChat.Server.Api.Domain.Chat.Repositories;
using StellarChat.Shared.Abstractions.Contracts.Chat;

namespace StellarChat.Server.Api.Features.Chat.GetChatSession;

internal sealed class GetChatSessionHandler : IQueryHandler<GetChatSession, ChatSessionResponse>
{
    private readonly IChatSessionRepository _chatSessionRepository;

    public GetChatSessionHandler(IChatSessionRepository chatSessionRepository) 
        => _chatSessionRepository = chatSessionRepository;

    public async ValueTask<ChatSessionResponse> Handle(GetChatSession query, CancellationToken cancellationToken)
        => (await _chatSessionRepository.GetAsync(query.Id))
            .Adapt<ChatSessionResponse>() ?? throw new ChatSessionNotFoundException(query.Id);
}
