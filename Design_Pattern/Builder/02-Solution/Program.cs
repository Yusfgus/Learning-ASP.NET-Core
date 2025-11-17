using System;
using Shared;

namespace Design_Pattern.Builder.Solution;

// 2- Director
class Program
{
    static void Main(string[] args)
    {
        Utils.printTitle("Design Pattern", width: 80, color: ConsoleColor.Red);
        Utils.printTitle("Builder ( Solution )", width: 70, color: ConsoleColor.Blue);

        Property property1 = new PropertyBuilder()
                            .SetAddress("123 Main st.")
                            .WithTransactionType(TransactionType.Sale)
                            .WithPropertyType(PropertyType.House)
                            .SetPrice(400_000)
                            .SetTotalBathrooms(4)
                            .SetFloorArea(190.0)
                            .SetTotalBathrooms(3)
                            .SetTotalStoreys(2)
                            .SetYearBuilt(2019)
                            .HasGym(false)
                            .HasSwimmingPool(true)
                            .HasWifi(false)
                            .HasParking(true)
                            .HasPlayground(true)
                            .Build();

        Console.WriteLine(property1);
        Console.WriteLine("----------------------------");

        Property property2 = new PropertyBuilder()
                            .SetAddress("452 Main st.")
                            .WithPropertyType(PropertyType.Apartment)
                            .WithTransactionType(TransactionType.Rent)
                            .SetPrice(2_000)
                            .Build();

        Console.WriteLine(property2);
        Console.WriteLine("----------------------------");

        Property property3 = new PropertyBuilder()
                            .SetAddress("452 Main st.")
                            .WithPropertyType(PropertyType.Apartment)
                            .WithTransactionType(TransactionType.Rent)
                            .Build();

        Console.WriteLine(property3);
    }
}
