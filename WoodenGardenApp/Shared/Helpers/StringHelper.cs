namespace WoodenGardenApp.Shared.Helpers;

public static class StringHelper
{
    public static bool IsNullOrWhiteSpace(this string? text) => string.IsNullOrWhiteSpace(text);
}