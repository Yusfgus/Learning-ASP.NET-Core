using System;
using Shared;

namespace Design_Pattern.Decorator.Problem;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Patter", 80, ConsoleColor.Red);
        Utils.printTitle("Decorator ( Problem )", 70, ConsoleColor.Blue);

        var order = new IceCreamWithSprinkles();
        Console.WriteLine(order);

        // the problem appears with combinations 
        // Ice Cream + sprinkles + fruit
        // Ice Cream + sprinkles + chocolate chips
    }
}
