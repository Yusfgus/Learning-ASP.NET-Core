using Shared;

namespace Linq;

public class Sorting
{
    public static void OrderBy()
    {
        Utils.printTitle(title: "Sorting ( OrderBy, OrderByDescending, ThenBy, ThenByDescending )", color: ConsoleColor.Blue, width: 70);

        string[] fruits = { "apricot", "orange", "banana", "mango", "apple", "grape", "strawberry" };

        IOrderedEnumerable<string> result01 = fruits.OrderBy(f => f); // Ascending by ASCII
        // IOrderedEnumerable<string> result01 = from f in fruits
        //                                orderby f /*ascending*/
        //                                select f;
        result01.Print("Fruits in Ascending order by ASCII");

        //=======================================================================================================

        IOrderedEnumerable<string> result02 = fruits.OrderBy(f => f.Length); // Ascending by length
        result02.Print("Fruits in Ascending order by Length");

        //=======================================================================================================

        IOrderedEnumerable<string> result03 = fruits.OrderByDescending(f => f.Length); // Descending by length
        // IOrderedEnumerable<string> result03 = from f in fruits
        //                                orderby f.Length descending
        //                                select f;
        result03.Print("Fruits in Descending order by Length");

        //=======================================================================================================

        IOrderedEnumerable<string> result04 = fruits.OrderBy(f => f.Length).ThenBy(f => f); // Ascending by length then by ASCII
        result04.Print("Fruits in Ascending order by Length then by ASCII");

        //=======================================================================================================

        IOrderedEnumerable<string> result05 = fruits.OrderBy(f => f.Length).ThenByDescending(f => f); // Ascending by length then Descending by ASCII
        result05.Print("Fruits in Ascending order by Length then Descending by ASCII");
    }

    public static void Reverse()
    {
        Utils.printTitle(title: "Sorting ( Reverse )", color: ConsoleColor.Blue, width: 70);

        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9};

        IEnumerable<int> result01 = numbers.Reverse();

        result01.Print("Numbers in reverse");
    }

    public static void OrderBy_with_Comparer()
    {
        Utils.printTitle(title: "Sorting ( OrderBy with custom Comparer)", color: ConsoleColor.Blue, width: 70);

        string[] employeesNo = { "2017-FI-1343", "2014-AC-4574", "2018-IT-2345", "2020-IT-2740", "2014-AC-4423" };

        IOrderedEnumerable<string> result01 = employeesNo.OrderBy(no => no, new employeeComparer());
        result01.Print("Sorting Employees by Comparer");
    }
}

internal class employeeComparer: IComparer<string>
{
    public int Compare(string? no1, string? no2)
    {
        if (no1 == null)
            return no2 is not null ? -1 : 0;
        else if (no2 == null)
            return 1;

        string[] no1_split = no1.Split('-');
        string[] no2_split = no2.Split('-');

        int no1_year = Convert.ToInt32(no1_split[0]);
        int no2_year = Convert.ToInt32(no2_split[0]);
        int no1_seq = Convert.ToInt32(no1_split[2]);
        int no2_seq = Convert.ToInt32(no2_split[2]);

        if (no1_year == no2_year)
        {
            return no1_seq.CompareTo(no2_seq);
        }
        else
        {
            return no1_year.CompareTo(no2_year);
        }
    }
}