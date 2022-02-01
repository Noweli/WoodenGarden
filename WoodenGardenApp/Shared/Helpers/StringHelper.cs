namespace WoodenGardenApp.Shared.Helpers;

public static class StringHelper
{
    public static bool IsNullOrWhiteSpace(this string? text) => string.IsNullOrWhiteSpace(text);
    public static string Format(this string format, params object[] arguments) => string.Format(format, arguments);
}