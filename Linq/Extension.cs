public static class Extension
{
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate) //  Func<Employee, bool> predicate =  Predicate<Employee> predicate 
    {
        foreach (var employee in source)
        {
            if (predicate(employee))
            {
                yield return employee;
            }
        }
    }

    public static void Print<T>(this IEnumerable<T> source, string title)
    {
        if (source == null)
            return;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("  ┌────────────────────────────────────────────────────────────┐");
        Console.WriteLine($"  │{title.Center(60)}│");
        Console.WriteLine("  └────────────────────────────────────────────────────────────┘");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;

        Console.Write("{ ");
        if (typeof(T).IsValueType || typeof(T) == typeof(string))
        {
            foreach (var item in source)
                Console.Write($"{item}, ");
        }
        else
        {
            foreach (var item in source)
                Console.WriteLine(item);
        }
        Console.WriteLine("}");
        
    }
    public static string Center(this string text, int width, char c = ' ')
    {
        if (text.Length >= width)
            return text;

        int rightSpaces = (width - text.Length) / 2 + (width - text.Length) % 2;
        return text.PadLeft(width - rightSpaces, c).PadRight(width, c);
    }

}
