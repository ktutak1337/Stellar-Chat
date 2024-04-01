using Mediator;

namespace StellarChat.Server.Api.Features.Chat.ChangeChatSessionTitle;

internal record ChangeChatSessionTitle(Guid ChatId, string Title) : ICommand;
