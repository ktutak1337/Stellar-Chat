namespace StellarChat.Server.Api.Domain.Settings.Repositories;

public interface ISettingsRepository
{
    ValueTask<AppSettings> GetSettingsByKeyAsync(string key);
    ValueTask UpdateAsync(AppSettings settings);
}
