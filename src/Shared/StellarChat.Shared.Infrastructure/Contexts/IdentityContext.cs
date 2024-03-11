using StellarChat.Shared.Abstractions.Contexts;
using System.Security.Claims;

namespace StellarChat.Shared.Infrastructure.Contexts;

public class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string? Role { get; }
    public Dictionary<string, IEnumerable<string>>? Claims { get; }

    private IdentityContext() { }

    public IdentityContext(Guid? id)
    {
        Id = id ?? Guid.Empty;
        IsAuthenticated = id.HasValue;
        Role = null;
        Claims = null;
    }

    public IdentityContext(ClaimsPrincipal? principal)
    {
        if (principal?.Identity is null || string.IsNullOrWhiteSpace(principal.Identity?.Name))
        {
            IsAuthenticated = false;
            Id = Guid.Empty;
            Role = null;
            Claims = null;
            return;
        }

        IsAuthenticated = principal.Identity.IsAuthenticated;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
        Role = principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        Claims = principal.Claims.GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
    }

    public static IIdentityContext Empty => new IdentityContext();
}

