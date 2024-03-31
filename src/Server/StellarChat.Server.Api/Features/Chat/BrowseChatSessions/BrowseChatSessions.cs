using StellarChat.Shared.Abstractions.Contracts.Chat;
using StellarChat.Shared.Abstractions.Pagination;

namespace StellarChat.Server.Api.Features.Chat.BrowseChatSessions;

internal sealed class BrowseChatSessions : PagedQuery<ChatSessionResponse> { }
