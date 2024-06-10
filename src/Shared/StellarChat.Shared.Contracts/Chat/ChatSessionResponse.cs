using StellarChat.Shared.Contracts.Assistants;

namespace StellarChat.Shared.Contracts.Chat;

public record ChatSessionResponse(Guid Id, Guid AssistantId, AssistantResponse AssignedAssistant, string Title, string Metaprompt, HashSet<string> ActivePlugins, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt);
