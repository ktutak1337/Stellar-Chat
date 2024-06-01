using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Settings;

public sealed record UpdateProfileRequest([property: JsonIgnore] string SettingsKey, string Name, string AvatarUrl, string Description);
