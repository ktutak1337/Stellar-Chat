namespace StellarChat.Server.Api.Features.Settings.UpdateIntegrations;

internal sealed class UpdateIntegrationsHandler : ICommandHandler<UpdateIntegrations>
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<UpdateIntegrationsHandler> _logger;

    public UpdateIntegrationsHandler(ISettingsRepository settingsRepository, TimeProvider clock, ILogger<UpdateIntegrationsHandler> logger)
    {
        _settingsRepository = settingsRepository;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(UpdateIntegrations command, CancellationToken cancellationToken)
    {
        var (key, integrations) = command;

        // Retrieve app settings based on the assumption that app settings always exist in the app.  
        var appSettings = await _settingsRepository.GetSettingsByKeyAsync(key);
        
        appSettings.Integrations = integrations;
        appSettings.UpdatedAt = _clock.GetLocalNow();
        
        await _settingsRepository.UpdateAsync(appSettings);
        _logger.LogInformation($"App settings for section 'Integrations' have been updated.");
        
        return Unit.Value;
    }
}
