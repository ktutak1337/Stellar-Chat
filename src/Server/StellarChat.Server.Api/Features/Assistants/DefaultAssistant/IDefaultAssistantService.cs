namespace StellarChat.Server.Api.Features.Assistants.DefaultAssistant;

internal interface IDefaultAssistantService
{
    ValueTask UnsetDefaultAsync();
    ValueTask SetDefaultAsync(Assistant assistant);
}
