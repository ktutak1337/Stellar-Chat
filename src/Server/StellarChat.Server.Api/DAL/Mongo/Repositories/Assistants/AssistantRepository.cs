namespace StellarChat.Server.Api.DAL.Mongo.Repositories.Assistants;

internal class AssistantRepository : IAssistantRepository
{
    private readonly IMongoRepository<AssistantDocument, Guid> _repository;

    public AssistantRepository(IMongoRepository<AssistantDocument, Guid> repository) 
        => _repository = repository;

    public async ValueTask<Assistant?> GetAsync(Guid id)
        => (await _repository.GetAsync(document => document.Id == id)).Adapt<Assistant>();

    public async ValueTask<IEnumerable<Assistant>> BrowseAsync()
        => (await _repository.FindAsync(_ => true)).Adapt<IEnumerable<Assistant>>();

    public async ValueTask AddAsync(Assistant assistant)
        => await _repository.AddAsync(assistant.Adapt<AssistantDocument>());

    public async ValueTask UpdateAsync(Assistant assistant)
        => await _repository.UpdateAsync(assistant.Adapt<AssistantDocument>());

    public async ValueTask DeleteAsync(Guid id)
        => await _repository.DeleteAsync(document => document.Id == id);

    public async ValueTask<bool> ExistsAsync(Guid id)
        => await _repository.ExistsAsync(document => document.Id == id);
}
