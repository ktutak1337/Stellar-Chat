namespace StellarChat.Server.Api.DAL.Mongo.Repositories.Actions;

internal class NativeActionRepository : INativeActionRepository
{

    private readonly IMongoRepository<NativeActionDocument, Guid> _repository;

    public NativeActionRepository(IMongoRepository<NativeActionDocument, Guid> repository) 
        => _repository = repository;

    public async ValueTask<NativeAction?> GetAsync(Guid id)
        => (await _repository.GetAsync(document => document.Id == id)).Adapt<NativeAction>();

    public async ValueTask<IEnumerable<NativeAction>> BrowseAsync()
        => (await _repository.FindAsync(_ => true)).Adapt<IEnumerable<NativeAction>>();

    public async ValueTask AddAsync(NativeAction nativeAction)
        => await _repository.AddAsync(nativeAction.Adapt<NativeActionDocument>());

    public async ValueTask UpdateAsync(NativeAction nativeAction)
        => await _repository.UpdateAsync(nativeAction.Adapt<NativeActionDocument>());

    public async ValueTask DeleteAsync(Guid id)
        => await _repository.DeleteAsync(document => document.Id == id);

    public async ValueTask<bool> ExistsAsync(Guid id)
        => await _repository.ExistsAsync(document => document.Id == id);
}
