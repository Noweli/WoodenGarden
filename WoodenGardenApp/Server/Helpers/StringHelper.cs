namespace WoodenGardenApp.Server.Helpers;

internal static class StringHelper
{
    internal static bool IsNullOrWhiteSpace(this string? text) => string.IsNullOrWhiteSpace(text);
}