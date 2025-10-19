class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World!", color: ConsoleColor.Red, width: 80);

        // Filtering ( Where )
        // Filtering.Where();

        // Projection ( Select, SelectMany, Zip )
        // Projection.Select();
        // Projection.SelectMany();
        // Projection.Zip();

        // Sorting ( OrderBy, OrderByDescending, ThenBy, ThenByDescending, Comparer, Reverse )
        // Sorting.OrderBy();
        // Sorting.OrderBy_with_Comparer();
        Sorting.Reverse();


        // Console.ReadKey();
        Console.WriteLine();
    }
}