using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Settings;

public sealed record UpdateIntegrationsRequest([property: JsonIgnore] string SettingsKey, List<Integration> Integrations);
