namespace StellarChat.Server.Api.Features.Settings.GetSettings;

internal sealed record GetSettings : IQuery<AppSettingsResponse>
{
    public string Key { get; set; } = string.Empty;
}
