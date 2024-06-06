namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal record Ask(Guid ChatId, Guid? AssistantId, string Message, string MessageType, string Model) : ICommand<string>;
