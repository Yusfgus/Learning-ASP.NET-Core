

public class Element
{

    private static string[] elems = { "elem1", "elem2", "elem3", "elem4", "elem5", "elem6", "elem7", "elem8", "elem9", "elem10", "elem11", "elem100" };

    public static void ElementAt()
    {
        Utils.printTitle(title: "Element ( ElementAt )", color: ConsoleColor.Blue, width: 70);

        string elem01 = elems.ElementAt(3);

        Utils.printTitle("Element at position 3");
        Console.WriteLine(elem01);

        //========================================================================================

        Utils.printTitle("Element at position 20");
        try
        {
            string elem02 = elems.ElementAt(20);  // ArgumentOutOfRangeException: Index was out of range.
            Console.WriteLine(elem02);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        //========================================================================================

        string? elem03 = elems.ElementAtOrDefault(20);

        Utils.printTitle("Element at position 20 or Default");

        if (elem03 is not null)
            Console.WriteLine(elem03);
        else
            Console.WriteLine("Null");
    }

    public static void First()
    {
        Utils.printTitle(title: "Element ( First )", color: ConsoleColor.Blue, width: 70);

        string elem01 = elems.First();

        Utils.printTitle("First element");
        Console.WriteLine(elem01);

        //========================================================================================

        string elem02 = elems.First(e => e.Length > 5);

        Utils.printTitle("First element with lenght > 5");
        Console.WriteLine(elem02);

        //========================================================================================

        Utils.printTitle("First element with lenght < 2");

        try
        {
            string elem03 = elems.First(e => e.Length < 2);  // InvalidOperationException: Sequence contains no matching element
            Console.WriteLine(elem03);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        //========================================================================================

        string? elem04 = elems.FirstOrDefault(e => e.Length < 2);

        Utils.printTitle("First element with lenght < 2");

        if (elem04 is not null)
            Console.WriteLine(elem04);
        else
            Console.WriteLine("Null");
    }

    public static void Last()
    {
        Utils.printTitle(title: "Element ( Last )", color: ConsoleColor.Blue, width: 70);

        string elem01 = elems.Last();

        Utils.printTitle("Last element");
        Console.WriteLine(elem01);

        //========================================================================================

        string elem02 = elems.Last(e => e.Length < 6);

        Utils.printTitle("Last element with lenght < 6");
        Console.WriteLine(elem02);

        //========================================================================================

        Utils.printTitle("Last element with lenght < 2");

        try
        {
            string elem03 = elems.Last(e => e.Length < 2);  // InvalidOperationException: Sequence contains no matching element
            Console.WriteLine(elem03);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        //========================================================================================

        string? elem04 = elems.LastOrDefault(e => e.Length < 2);

        Utils.printTitle("Last element with lenght < 2");

        if (elem04 is not null)
            Console.WriteLine(elem04);
        else
            Console.WriteLine("Null");
    }
    
    public static void Single()
    {
        Utils.printTitle(title: "Element ( Single )", color: ConsoleColor.Blue, width: 70);

        string elem01 = elems.Single(e => e.Length > 6);

        Utils.printTitle("Single element with lenght > 6");
        Console.WriteLine(elem01);

        //========================================================================================

        Utils.printTitle("Single element");

        try
        {
            string elem02 = elems.Single(); // InvalidOperationException: Sequence contains more than one element
            Console.WriteLine(elem02);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        //========================================================================================

        Utils.printTitle("Single element with lenght < 6");

        try
        {
            string elem03 = elems.Single(e => e.Length < 6);  // InvalidOperationException: Sequence contains more than one matching element
            Console.WriteLine(elem03);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        //========================================================================================

        // Note: SingleOrDefault helps only when there's no matching element

        string? elem04 = elems.SingleOrDefault(e => e.Length < 2);

        Utils.printTitle("Single element with lenght < 2");

        if (elem04 is not null)
            Console.WriteLine(elem04);
        else
            Console.WriteLine("Null");
    }

}