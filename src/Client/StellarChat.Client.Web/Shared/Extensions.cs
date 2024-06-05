namespace StellarChat.Client.Web.Shared;

public static class Extensions
{
    public static bool IsEmpty(this string value)
        => string.IsNullOrWhiteSpace(value);

    public static bool IsNotEmpty(this string value)
        => !value.IsEmpty();
}
