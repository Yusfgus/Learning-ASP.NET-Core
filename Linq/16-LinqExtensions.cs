
public class LinqExtensions
{
    private static string[] items = { "item01", "item02", "item03", "item04", "item05", "item06", "item07", "item08", "item09", "item10" };

    public static void Paginate()
    {
        Utils.printTitle(title: "Linq Extensions ( Paginate )", color: ConsoleColor.Blue, width: 70);
        
        int page = 2;
        int pageSize = 5;

        IEnumerable<string> result = items.Paginate(null, pageSize);

        result.Print("Paginate Items");
    }
}