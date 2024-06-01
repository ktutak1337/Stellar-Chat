namespace StellarChat.Server.Api.DAL.Mongo.Repositories.Settings;

internal sealed class SettingsRepository : ISettingsRepository
{
    private readonly IMongoRepository<AppSettingsDocument, Guid> _repository;

    public SettingsRepository(IMongoRepository<AppSettingsDocument, Guid> repository) 
        => _repository = repository;

    public async ValueTask<AppSettings> GetSettingsByKeyAsync(string key)
        => (await _repository.GetAsync(document => document.Key == key)).Adapt<AppSettings>();

    public async ValueTask UpdateAsync(AppSettings settings)
        => await _repository.UpdateAsync(settings.Adapt<AppSettingsDocument>());
}
