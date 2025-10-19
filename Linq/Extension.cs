public static class Extension
{
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate) //  Func<Employee, bool> predicate =  Predicate<Employee> predicate 
    {
        foreach (var employee in source)
        {
            if (predicate(employee))
            {
                yield return employee;
            }
        }
    }

    public static void Print<T>(this IEnumerable<T> source, string title = "")
    {
        if (source == null)
            return;

        Utils.printTitle(title);

        Console.Write("{ ");
        if (typeof(T).IsValueType)
        {
            foreach (var item in source)
                Console.Write($"{item}, ");
        }
        else if(typeof(T) == typeof(string))
        {
            foreach (var item in source)
                Console.Write($"'{item}', ");
        }
        else
        {
            foreach (var item in source)
                Console.WriteLine(item);
        }
        Console.WriteLine("}");

    }
}
