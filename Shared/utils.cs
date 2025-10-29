namespace Shared;

public static class Utils
{
    public static void printTitle(string title, int width = 60, ConsoleColor color = ConsoleColor.Green)
    {
        width = Math.Max(width, title.Length + 4);

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

        int leftSpaces = (width - text.Length) / 2;
        return text.PadLeft(width - leftSpaces, c).PadRight(width, c);
    }
}