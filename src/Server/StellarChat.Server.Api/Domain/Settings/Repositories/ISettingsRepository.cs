namespace StellarChat.Server.Api.Domain.Settings.Repositories;

public interface ISettingsRepository
{
    ValueTask<AppSettings> GetSettingsByKeyAsync(string title);
    ValueTask UpdateAsync(AppSettings settings);
}
