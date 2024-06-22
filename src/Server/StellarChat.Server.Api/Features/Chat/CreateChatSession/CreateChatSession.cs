namespace StellarChat.Server.Api.Features.Chat.CreateChatSession;

internal sealed record CreateChatSession([Required] Guid ChatId, Guid AssistantId, string Title, string Message) : ICommand;
