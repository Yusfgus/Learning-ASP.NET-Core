

public class Concatenation
{
    public static void Concat()
    {
        Utils.printTitle(title: "Concatenation ( Concat )", color: ConsoleColor.Blue, width: 70);

        int[] numbers01 = { 1, 2, 3, 4, 5 };
        List<int> numbers02 = new() { 6, 7, 8, 9, 10 };

        IEnumerable<int> result = numbers01.Concat(numbers02);

        result.Print("Array Concat with List");
    }
}