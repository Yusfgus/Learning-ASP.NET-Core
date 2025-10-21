


public class Grouping
{
    private static int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public static void GroupBy()
    {
        // Uses deferred execution
        // each iterate => group again (used for single process)
        // returns IEnumerable<IGrouping<TKey, TSource>>

        Utils.printTitle(title: "Grouping ( GroupBy )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<IGrouping<int, int>> groups01 = numbers.GroupBy(x => x % 3);

        groups01.printGroups("Group by mod 3");

        numbers[0] = 0;

        groups01.printGroups("Group by mod 3 after update"); // saw the update
    }

    public static void ToLookup()
    {
        // Uses immediate execution
        // buffer the result in memory (used for multiple process)
        // returns ILook<TKey, TSource>

        Utils.printTitle(title: "Grouping ( ToLookup )", color: ConsoleColor.Blue, width: 70);

        ILookup<int, int> groups01 = numbers.ToLookup(x => x % 3);

        groups01.printGroups("Group by mod 3");

        numbers[0] = 0;

        groups01.printGroups("Group by mod 3 after update");  // didn't see the update
    }
}