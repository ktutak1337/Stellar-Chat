using Mapster;
using StellarChat.Server.Api.DAL.Mongo.Documents.Chat;
using StellarChat.Server.Api.Domain.Chat.Models;
using StellarChat.Server.Api.Domain.Chat.Repositories;
using StellarChat.Shared.Infrastructure.DAL.Mongo;

namespace StellarChat.Server.Api.DAL.Mongo.Repositories.Chat;

internal class ChatSessionRepository : IChatSessionRepository
{
    private readonly IMongoRepository<ChatSessionDocument, Guid> _repository;

    public ChatSessionRepository(IMongoRepository<ChatSessionDocument, Guid> repository) => _repository = repository;

    public async ValueTask<ChatSession?> GetAsync(Guid id)
        => (await _repository.GetAsync(id)).Adapt<ChatSession>();

    public async ValueTask<ChatSession?> GetByTitleAsync(string title)
        => (await _repository.GetAsync(document => document.Title == title)).Adapt<ChatSession>();

    public async ValueTask<IEnumerable<ChatSession>> BrowseAsync()
        => (await _repository.FindAsync(_ => true)).Adapt<IEnumerable<ChatSession>>();

    public async ValueTask AddAsync(ChatSession chatSession)
        => await _repository.AddAsync(chatSession.Adapt<ChatSessionDocument>());

    public async ValueTask UpdateAsync(ChatSession chatSession)
        => await _repository.UpdateAsync(chatSession.Adapt<ChatSessionDocument>());

    public async ValueTask DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);

    public async ValueTask<bool> ExistsAsync(Guid id)
        => await _repository.ExistsAsync(document => document.Id == id);
}
