namespace StellarChat.Shared.Contracts.Chat;

public record ChatSessionResponse(Guid Id, string Title, string Metaprompt, HashSet<string> ActivePlugins, string AvatarUrl, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt);
