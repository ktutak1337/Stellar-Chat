namespace StellarChat.Server.Api.Domain.Chat.Models;

internal static class Author
{
    public const string Bot = "bot";
    public const string User = "user";

    public static bool IsValid(string author)
    {
        if (author.IsEmpty())
        {
            return false;
        }

        author = author.ToLowerInvariant();

        return author == User || author == Bot;
    }
}
