using Shared;

namespace Linq;

public class LinqAnatomy
{
    public static void ExecutionOrder()
    {
        Utils.printTitle(title: "Linq Anatomy ( Execution Order )", color: ConsoleColor.Blue, width: 70);
 
        // Left to right(All Expression in C# are executed Left to Right)
        // Understanding the semantics of query execution, can lead to some meaningful optimizations
        // Where is not required to find all matching items before fetching the first matching item. 
        // Where fetches matching items "on demand"
        // IEnumerable / foreach / yield Element are not returned at once / one at a time

        var numbers = new int[] { 8, 2, 3, 4, 1, 6, 5, 12, 9 };

        var query = numbers
            .Where(x =>
            {
                Console.WriteLine($"Where({x} > 5) => {x > 5}");
                return x > 5;
            })
            .Select(x =>
            {
                Console.WriteLine($"\tSelect({x} X {x}) => {x * x}");
                return x * x;
            })
            .Where(x =>
            {
                var result = x % 6 == 0;
                Console.WriteLine($"\t\tWhere({x} % 6) == 0 => {result}");
                if (result)
                    Console.WriteLine($"\t\t\tTake: {x}");


                return x % 6 == 0;
            })
            .Take(2);

        var list = query.ToList();

        foreach (var item in list)
            Console.Write($" {item}");
    }
}