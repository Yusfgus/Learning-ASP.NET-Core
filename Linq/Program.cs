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

        #region Quantifiers ( Any, All, Contains )
        // Quantifiers.Any(); 
        // Quantifiers.All();
        // Quantifiers.Contains();
        #endregion

        #region Grouping ( GroupBy, ToLookup )
        // Grouping.GroupBy();
        // Grouping.ToLookup();
        #endregion

        #region Joining ( Join, GroupJoin )
        // Joining.Join();
        // Joining.GroupJoin();
        #endregion

        #region Generation Operations ( Empty, DefaultIfEmpty, Range, Repeat )
        // Generation.Empty();
        // Generation.DefaultIfEmpty();
        // Generation.Range();
        // Generation.Repeat();
        #endregion

        #region Element Operations( ElementAt, First, FirstOrDefault, Last, LastOrDefault, Single, SingleOrDefault )
        // Element.ElementAt();
        // Element.First();
        // Element.Last();
        // Element.Single();
        #endregion

        // Console.ReadKey();
        Console.WriteLine();
    }
}