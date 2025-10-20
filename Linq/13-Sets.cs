

public class Sets
{
    private static string[] fruits01 = { "manga", "apple", "banana", "apple", "orange", "strewberry" };
    private static List<string> fruits02 = new() { "apple", "manga", "watermellon" };

    public static void Distict()
    {
        Utils.printTitle(title: "Sets ( Distict, DistictBy )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> result01 = fruits01.Distinct(); // uses Equals()
        result01.Print("Distict fruits");

        //===========================================================================================

        IEnumerable<string> result02 = fruits01.DistinctBy(x => x.Length);
        result02.Print("Distict lengths of fruits");
    }

    public static void Except()
    {
        Utils.printTitle(title: "Sets ( Except, ExceptBy )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> result01 = fruits01.Except(fruits02); // uses Equals()
        result01.Print("Fruits01 except Fruits02");

        //===========================================================================================

        IEnumerable<string> result02 = fruits01.ExceptBy(fruits02.Select(f2 => f2.Length), f1 => f1.Length);
        result02.Print("Fruits01 except Fruits02 by Length");
    }
    
    public static void Intersect()
    {
        Utils.printTitle(title: "Sets ( Intersect, IntersectBy )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> result01 = fruits01.Intersect(fruits02); // uses Equals()
        result01.Print("Fruits01 Intersect with Fruits02");

        //===========================================================================================

        IEnumerable<string> result02 = fruits01.IntersectBy(fruits02.Select(f2 => f2.Length), f1 => f1.Length);
        result02.Print("Fruits01 Intersect with Fruits02 by Lenght");
    }
    
    public static void Union()
    {
        // same as Concate but without repetition

        Utils.printTitle(title: "Sets ( Intersect, IntersectBy )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> result01 = fruits01.Union(fruits02); // uses Equals()
        result01.Print("Fruits01 Union with Fruits02");

        //===========================================================================================

        IEnumerable<string> result02 = fruits01.UnionBy(fruits02, f1 => f1.Length);
        result02.Print("Fruits01 Union with Fruits02 by Lenght");
    }
} 