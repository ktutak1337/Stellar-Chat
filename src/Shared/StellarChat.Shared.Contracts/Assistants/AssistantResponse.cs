namespace StellarChat.Shared.Contracts.Assistants;

public record AssistantResponse(
    Guid Id,
    string Name,
    string Metaprompt, 
    string Description,
    string AvatarUrl,
    string DefaultModel,
    string DefaultVoice,
    bool IsDefault,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
