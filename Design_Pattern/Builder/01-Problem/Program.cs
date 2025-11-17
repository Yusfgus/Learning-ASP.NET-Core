using System;

namespace Design_Pattern.Builder.Problem;

class Program
{
    static void Main(string[] args)
    {
        var property1 = new Property("123 Main st.", TransactionType.Sale, PropertyType.House
            , 4, 190.0, 400_000m, 3, 2, 2019, true, false, false, true, true);

        Console.WriteLine(property1);

        Console.WriteLine("------");

        var property2 = new Property("452 Main st.", TransactionType.Rent, PropertyType.Apartment
        , 400_000m);

        Console.WriteLine(property2);
    }
}
