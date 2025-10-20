

public class Generation
{
    public static void Empty()
    {
        Utils.printTitle(title: "Generation ( Empty )", color: ConsoleColor.Blue, width: 70);

        // bad
        List<string> items01 = new List<string>(); // empty list
        //1
        //...
        //1000
        foreach (string item in items01) // used after 1000 line
            Console.WriteLine(item);


        // better
        IEnumerable<string> items02 = Enumerable.Empty<string>();
        //1
        //...
        //1000
        foreach (string item in items02)
            Console.WriteLine(item);
    }

    public static void DefaultIfEmpty()
    {
        Utils.printTitle(title: "Generation ( DefaultIfEmpty )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> items01 = Enumerable.Empty<string>();
        items01.Print("Items 01");

        IEnumerable<string?> items02 = items01.DefaultIfEmpty();
        items02.Print("Items 02");

        IEnumerable<string?> items03 = items01.DefaultIfEmpty("default");
        items03.Print("Items 03");

    }

    public static void Range()
    {
        Utils.printTitle(title: "Generation ( Range )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<int> range = Enumerable.Range(1, 10); // deferred execution

        range.Print("Range from 1 to 10");
    }

    public static void Repeat()
    {
        Utils.printTitle(title: "Generation ( Repeat )", color: ConsoleColor.Blue, width: 70);

        string name = "gus";

        IEnumerable<string> names = Enumerable.Repeat(name, 10);

        names.Print("Repeat name 10 times");

        Console.WriteLine($"\nnames[0] equals names[1]? {ReferenceEquals(names.ToList()[0], names.ToList()[1])}"); // although they're reference type
    }

}