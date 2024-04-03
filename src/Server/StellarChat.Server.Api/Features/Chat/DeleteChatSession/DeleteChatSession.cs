using Mediator;

namespace StellarChat.Server.Api.Features.Chat.DeleteChatSession;

internal record DeleteChatSession(Guid ChatId) : ICommand;
