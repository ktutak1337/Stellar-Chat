using Mediator;
using System.ComponentModel.DataAnnotations;

namespace StellarChat.Server.Api.Features.Chat.CreateChatSession;

internal sealed record CreateChatSession([Required] Guid ChatId, string Title) : ICommand;
