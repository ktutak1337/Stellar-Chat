namespace StellarChat.Shared.Contracts.Chat;

public record ChatSessionResponse(Guid Id, string Title, string Metaprompt, HashSet<string> ActivePlugins, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt);
