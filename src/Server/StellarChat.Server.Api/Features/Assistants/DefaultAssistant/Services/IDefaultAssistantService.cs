namespace StellarChat.Server.Api.Features.Assistants.DefaultAssistant.Services;

internal interface IDefaultAssistantService
{
    ValueTask RevokeCurrentAsync();
    ValueTask SetAsDefaultAsync(Assistant assistant, bool saveChanges = true);
    bool IsCurrentlyDefault(Assistant assistant);
}
