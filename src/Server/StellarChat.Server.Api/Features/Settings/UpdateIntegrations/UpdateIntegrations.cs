using Integration = StellarChat.Server.Api.Domain.Settings.Models.Integration;

namespace StellarChat.Server.Api.Features.Settings.UpdateIntegrations;

internal sealed record UpdateIntegrations(string SettingsKey, List<Integration> Integrations) : ICommand;
