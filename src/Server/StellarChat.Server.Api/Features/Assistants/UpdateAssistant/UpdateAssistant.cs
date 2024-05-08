namespace StellarChat.Server.Api.Features.Assistants.UpdateAssistant;

internal record UpdateAssistant(
    [Required] Guid Id,
    string Name,
    string Metaprompt,
    string Description,
    string AvatarUrl,
    string DefaultModel,
    string DefaultVoice,
    bool IsDefault) : ICommand;