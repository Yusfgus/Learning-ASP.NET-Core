class Program
{
    static void Main()
    {
        Utils.printTitle(title: "Hello World!", color: ConsoleColor.Red, width: 80);

        #region Filtering ( Where )
        // Filtering.Where();
        #endregion

        #region Projection ( Select, SelectMany, Zip )
        // Projection.Select();
        // Projection.SelectMany();
        // Projection.Zip();
        #endregion

        #region Sorting ( OrderBy, OrderByDescending, ThenBy, ThenByDescending, Comparer, Reverse )
        // Sorting.OrderBy();
        // Sorting.OrderBy_with_Comparer();
        // Sorting.Reverse();
        #endregion

        #region Partitioning ( Skip, SkipLast, SkipWhile, Take, TakeLast, TakeWhile, Chunk )
        // Partitioning.Skip();
        // Partitioning.Take();
        // Partitioning.Chunk();
        #endregion

        // Console.ReadKey();
        Console.WriteLine();
    }
}