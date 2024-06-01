namespace StellarChat.Server.Api.Features.Settings.UpdateProfile;

internal sealed record UpdateProfile(string SettingsKey, string Name, string AvatarUrl, string Description) : ICommand;
