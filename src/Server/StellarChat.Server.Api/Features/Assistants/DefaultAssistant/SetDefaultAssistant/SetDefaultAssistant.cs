namespace StellarChat.Server.Api.Features.Assistants.DefaultAssistant.SetDefaultAssistant;

internal record SetDefaultAssistant(Guid Id, bool IsDefault) : ICommand;
