using System.Text.RegularExpressions;

namespace StellarChat.Shared.Infrastructure;

public static class Extensions
{
    public static string ToSnakeCase(this string input)
        => Regex.Replace(
            Regex.Replace(
                Regex.Replace(input, @"([\p{Lu}]+)([\p{Lu}][\p{Ll}])", "$1_$2"), @"([\p{Ll}\d])([\p{Lu}])", "$1_$2"), @"[-\s]", "_").ToLower();
}
