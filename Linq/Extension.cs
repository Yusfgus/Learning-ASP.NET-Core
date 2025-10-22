using System.Diagnostics;

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

        if (title != "")
            Utils.printTitle(title);

        if (typeof(T).IsValueType)
        {
            Console.Write("{ ");
            foreach (var item in source)
                Console.Write($"{item}, ");
        }
        else if (typeof(T) == typeof(string))
        {
            Console.Write("{ ");
            foreach (var item in source)
                Console.Write($"'{item}', ");
        }
        else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(typeof(T)))
        {
            Console.WriteLine("{");
            foreach (var collection in source)
            {
                System.Collections.IEnumerable? coll = collection as System.Collections.IEnumerable;
                if (coll is not null)
                {
                    Console.Write("\t{ ");
                    foreach (var item in coll)
                        Console.Write($"{item}, ");
                    Console.WriteLine("}");
                }
            }
        }
        else
        {
            Console.WriteLine("{");
            foreach (var item in source)
                Console.WriteLine($"\t{item}");
        }
        Console.WriteLine("}");

    }

    public static void printGroups<T>(this IEnumerable<IGrouping<T, T>> groups, string title = "")
    {
        if (title != "")
            Utils.printTitle(title);

        Console.WriteLine("{");
        foreach (IGrouping<T, T> group in groups)
        {
            Console.Write($"\t{group.Key}: ");
            group.Print();
        }
        Console.WriteLine("}");
    }

    public static IEnumerable<TSource> Paginate<TSource>(this IEnumerable<TSource> source, int? page, int? pageSize)
    {
        if (source is null)
            throw new ArgumentException(nameof(source));

        if (!page.HasValue)
            page = 1;
        else if (page <= 0)
            throw new ArgumentException(nameof(page));

        if (!pageSize.HasValue)
            pageSize = 10;
        else if (pageSize <= 0)
            throw new ArgumentException(nameof(pageSize));
            
        return source.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
    }

}
