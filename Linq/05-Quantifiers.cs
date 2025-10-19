


public class Quantifiers
{
    public static int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public static void Any()
    {
        Utils.printTitle(title: "Quantifiers ( Any )", color: ConsoleColor.Blue, width: 70);

        bool result01 = numbers.Any(n => n % 2 == 0 && n % 3 == 0);
        Console.WriteLine("Is there any number divisible by 2 and 3? " + result01);

        bool result02 = numbers.Any(n => n > 20);
        Console.WriteLine("Is there any number greater that 20? " + result02);
    }

    internal static void All()
    {
        Utils.printTitle(title: "Quantifiers ( All )", color: ConsoleColor.Blue, width: 70);

        bool result01 = numbers.All(n => n >= 0);
        Console.WriteLine("Are all numbers positive? " + result01);

        bool result02 = numbers.All(n => n % 2 == 0);
        Console.WriteLine("Are all numbers even? " + result02);
    }

    internal static void Contains()
    {
        // similar to Any but has O(1) complexity with hash set

        Utils.printTitle(title: "Quantifiers ( Contains )", color: ConsoleColor.Blue, width: 70);

        bool result01 = numbers.Contains(5);
        Console.WriteLine("Is there any number equal to 5? " + result01);
    }
}