namespace Shared;

public static class Utils
{
    public static void printTitle(string title, int width = 60, ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.WriteLine();
        Console.WriteLine("  ┌".PadRight(width + 3, '─') + "┐");
        Console.WriteLine($"  │{title.Center(width)}│");
        Console.WriteLine("  └".PadRight(width + 3, '─') + "┘");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private static string Center(this string text, int width, char c = ' ')
    {
        if (text.Length >= width)
            return text;

        int rightSpaces = (width - text.Length) / 2 + (width - text.Length) % 2;
        return text.PadLeft(width - rightSpaces, c).PadRight(width, c);
    }
}