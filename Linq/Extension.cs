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
        else if (typeof(T) == typeof(string))
        {
            foreach (var item in source)
                Console.Write($"'{item}', ");
        }
        else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(typeof(T)))
        {
            Console.WriteLine();
            foreach(var collection in source)
            {
                System.Collections.IEnumerable? coll = collection as System.Collections.IEnumerable;
                Console.Write("\t{ ");
                foreach(var item in coll)
                    Console.Write($"{item}, ");
                Console.WriteLine("}");
            }
        }
        else
        {
            foreach (var item in source)
                Console.WriteLine(item);
        }
        Console.WriteLine("}");

    }
}
