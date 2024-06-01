namespace StellarChat.Server.Api.Features.Settings.GetSettings;

internal sealed class GetSettingsHandler : IQueryHandler<GetSettings, AppSettingsResponse>
{
    private readonly ISettingsRepository _settingsRepository;

    public GetSettingsHandler(ISettingsRepository settingsRepository) 
        => _settingsRepository = settingsRepository;

    public async ValueTask<AppSettingsResponse> Handle(GetSettings query, CancellationToken cancellationToken)
        => (await _settingsRepository.GetSettingsByKeyAsync(query.Key)).Adapt<AppSettingsResponse>();
}
