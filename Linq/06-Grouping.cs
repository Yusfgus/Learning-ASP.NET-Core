


public class Grouping
{
    public static void GroupBy()
    {
        // Uses defferred execution
        // each iterate => group again (used for single process)
        // returns IEnumerable<IGrouping<TKey, TSource>>

        Utils.printTitle(title: "Grouping ( GroupBy )", color: ConsoleColor.Blue, width: 70);

        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        IEnumerable<IGrouping<int, int>> groups01 = numbers.GroupBy(x => x % 3);

        groups01.printGroups("Group by mod 3");

        numbers[0] = 0;

        groups01.printGroups("Group by mod 3 after update"); // saw the update
    }

    public static void ToLookup()
    {
        // Uses immediate execution
        // buffer the result in memeory (used for multiple process)
        // returns ILook<Tkey, TSource>

        Utils.printTitle(title: "Grouping ( ToLookup )", color: ConsoleColor.Blue, width: 70);

        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        ILookup<int, int> groups01 = numbers.ToLookup(x => x % 3);

        groups01.printGroups("Group by mod 3");

        numbers[0] = 0;

        groups01.printGroups("Group by mod 3 after update");  // didn't see the update
    }
}