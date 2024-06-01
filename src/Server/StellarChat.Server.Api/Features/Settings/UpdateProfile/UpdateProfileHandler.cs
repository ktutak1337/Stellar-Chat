namespace StellarChat.Server.Api.Features.Settings.UpdateProfile;

internal sealed class UpdateProfileHandler : ICommandHandler<UpdateProfile>
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly TimeProvider _clock;
    private readonly ILogger<UpdateProfileHandler> _logger;

    public UpdateProfileHandler(ISettingsRepository settingsRepository, TimeProvider clock, ILogger<UpdateProfileHandler> logger)
    {
        _settingsRepository = settingsRepository;
        _clock = clock;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(UpdateProfile command, CancellationToken cancellationToken)
    {
        var (key, name, avatarUrl, description) = command;

        // Retrieve app settings based on the assumption that app settings always exist in the app.
        var appSettings = await _settingsRepository.GetSettingsByKeyAsync(key);

        var now = _clock.GetLocalNow();

        appSettings.Profile.Name = name;
        appSettings.Profile.AvatarUrl = avatarUrl;
        appSettings.Profile.Description = description;
        appSettings.UpdatedAt = now;

        await _settingsRepository.UpdateAsync(appSettings);
        _logger.LogInformation($"App settings for section 'Profile' has been updated.");

        return Unit.Value;
    }
}
