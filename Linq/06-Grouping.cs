using Shared;

namespace Linq;

public class Grouping
{
    private static int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public static void GroupBy()
    {
        // Uses deferred execution
        // each iterate => group again (used for single process)
        // returns IEnumerable<IGrouping<TKey, TSource>>

        // ✔ Characteristics
        // Lazy → groups are created only when enumerated
        // One-time use → enumerating again may recompute the grouping
        // Not indexed → you cannot do group[key]
        // Does not guarantee immediate availability
        // Usually used once in a chain of LINQ methods

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

        // A Lookup is basically:
        // ✔ A read-only multi-dictionary
        // ✔ Key → multiple values

        // Like:
        // Dictionary<TKey, List<TElement>>

        // ✔ Characteristics
        // Eager → fully built immediately
        // Indexes → you can use lookup[key]
        // Fast lookups (O(1))
        // Safe if key does not exist (returns empty sequence, no exception)
        // Ideal for repeated access by key
        
        Utils.printTitle(title: "Grouping ( ToLookup )", color: ConsoleColor.Blue, width: 70);

        ILookup<int, int> groups01 = numbers.ToLookup(x => x % 3);

        groups01.printGroups("Group by mod 3");

        numbers[0] = 0;

        groups01.printGroups("Group by mod 3 after update");  // didn't see the update
    }
}