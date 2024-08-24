using System.Collections.ObjectModel;

namespace StellarChat.Shared.Infrastructure.Identity;

public static class StellarRoles
{
    public const string Basic = nameof(Basic);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
    Basic
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}
