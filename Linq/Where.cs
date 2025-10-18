
public class Where
{
    public static void Run()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        IEnumerable<int> evenNumbers01 = numbers.Filter(n => n % 2 == 0);
        evenNumbers01.Print("using Filter");

        IEnumerable<int> evenNumbers02 = numbers.Where(n => n % 2 == 0);
        evenNumbers02.Print("Using Where");

        IEnumerable<int> evenNumbers03 = Enumerable.Where(numbers, n => n % 2 == 0);
        evenNumbers03.Print("Using Enumerable.Where");

        IEnumerable<int> evenNumbers04 = from n in numbers 
                                        where n % 2 == 0 
                                        select n;
        evenNumbers03.Print("Using Query Syntax");

    }
}