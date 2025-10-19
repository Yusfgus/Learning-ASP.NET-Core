
public class Partitioning
{

    private static string[] items = { "item01", "item02", "item03", "item04", "item05", "item06", "item07", "item08", "item09", "item10" };

    public static void Skip()
    {
        Utils.printTitle(title: "Partitioning ( Skip, SkipLast, SkipWhile )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> result01 = items.Skip(4);
        result01.Print("Items Skip first 4");
        
        //============================================================================================
        
        IEnumerable<string> result02 = items.SkipLast(3);
        result02.Print("Items Skip last 3");

        //============================================================================================

        IEnumerable<string> result03 = items.SkipWhile(x => x != "item06");
        result03.Print("Items Skip until item06");
    }

    public static void Take()
    {
        Utils.printTitle(title: "Partitioning ( Take, TakeLast, TakeWhile )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> result01 = items.Take(4);
        result01.Print("Items Take first 4");
        
        //============================================================================================
        
        IEnumerable<string> result02 = items.TakeLast(3);
        result02.Print("Items Take last 3");

        //============================================================================================

        IEnumerable<string> result03 = items.TakeWhile(x => x != "item06");
        result03.Print("Items Take until item06");
    }

    public static void Chunk()
    {
        Utils.printTitle(title: "Partitioning ( Chunk )", color: ConsoleColor.Blue, width: 70);
        
        IEnumerable<string[]> result01 = items.Chunk(2);
        result01.Print("Items Chunks of 2");
    }
}