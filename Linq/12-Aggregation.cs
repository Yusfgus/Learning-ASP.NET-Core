

public class Aggregation
{
    private static int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private static string[] names = { "yousef", "mohamed", "sayed", "ahmed" };

    public static void Aggregate()
    {
        // same as reduce()

        Utils.printTitle(title: "Equality ( Aggregate )", color: ConsoleColor.Blue, width: 70);

        int mult = numbers.Aggregate((a, b) => a * b);

        Utils.printTitle("Multiplication of numbers");
        Console.WriteLine($"Multiply = {mult}");

        // ==========================================================================================

        // with start value
        int sub = numbers.Aggregate(100, (a, b) => a - b);

        Utils.printTitle("Subtraction of numbers from 100");
        Console.WriteLine($"Subtraction = {sub}");

        // ==========================================================================================

        string maxName = names.Aggregate((n, m) => n.Length >= m.Length ? n : m);

        Utils.printTitle("Name with maximum length");
        Console.WriteLine($"Max = '{maxName}'");

    }

    public static void Count() // Or LongCount
    {
        Utils.printTitle(title: "Equality ( Count )", color: ConsoleColor.Blue, width: 70);

        int count01 = numbers.Count();

        Utils.printTitle("Numbers Count");
        Console.WriteLine($"Count = {count01}");

        // ==========================================================================================

        int count02 = numbers.Count(x => x % 2 == 0);
        // int count02 = numbers.Where(x => x % 2 == 0).Count();

        Utils.printTitle("Even numbers Count");
        Console.WriteLine($"Count = {count02}");
    }

    public static void Max()
    {
        Utils.printTitle(title: "Equality ( Max, MaxBy)", color: ConsoleColor.Blue, width: 70);

        int max = numbers.Max();

        Utils.printTitle("Max number");
        Console.WriteLine($"Max = {max}");

        //=================================================================================

        int maxLen = names.Max(n => n.Length);

        Utils.printTitle("Maximum name length");
        Console.WriteLine($"Max = {maxLen}");

        // ==========================================================================================

        string? maxName = names.MaxBy(n => n.Length);

        Utils.printTitle("Name with maximum length");
        Console.WriteLine($"Max = '{maxName}'");
    }

    public static void Min()
    {
        Utils.printTitle(title: "Equality ( Min, MinBy )", color: ConsoleColor.Blue, width: 70);

        int min = numbers.Min();
        
        Utils.printTitle("Minimum number");
        Console.WriteLine($"Min = {min}");

        //=================================================================================

        int minLen = names.Min(n => n.Length);

        Utils.printTitle("Minimum name length");
        Console.WriteLine($"Min = {minLen}");

        // ==========================================================================================

        string? minName = names.MinBy(n => n.Length);

        Utils.printTitle("Name with minimum length");
        Console.WriteLine($"Max = '{minName}'");
    }

    public static void Sum()
    {
        Utils.printTitle(title: "Equality ( Sum )", color: ConsoleColor.Blue, width: 70);

        int sum = numbers.Sum();

        Utils.printTitle("Sumation of numbers");
        Console.WriteLine($"Sum = {sum}");

        //=================================================================================

        int sumLen = names.Sum(n => n.Length);

        Utils.printTitle("Sumation of names length");
        Console.WriteLine($"Sum = {sumLen}");
    }

    public static void Average()
    {
        Utils.printTitle(title: "Equality ( Average )", color: ConsoleColor.Blue, width: 70);

        double Avg = numbers.Average();
        
        Utils.printTitle("Average number");
        Console.WriteLine($"Average = {Avg}");

        //=================================================================================

        double avgLen = names.Average(n => n.Length);

        Utils.printTitle("Average name length");
        Console.WriteLine($"Average = {avgLen}");
    }
}