namespace StellarChat.Server.Api.Features.Assistants.CreateAssistant;

internal sealed record CreateAssistant(
    [Required] Guid Id,
    string Name,
    string Metaprompt,
    string Description,
    string AvatarUrl,
    string DefaultModel,
    string DefaultVoice,
    bool IsDefault) : ICommand;
