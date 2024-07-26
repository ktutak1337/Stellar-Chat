namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal record Ask(Guid ChatId, string Message, string MessageType, string Model, string ServiceId) : ICommand<string>;
