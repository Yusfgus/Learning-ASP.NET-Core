using System;
using Shared;

namespace Design_Pattern.Decorator.Solution;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Patter", 80, ConsoleColor.Red);
        Utils.printTitle("Decorator ( Solution )", 70, ConsoleColor.Blue);

        IceCream iceCream = new BasicIceCream();
        Console.WriteLine(iceCream);
        
        iceCream = new Sprinkles(iceCream);
        Console.WriteLine(iceCream);

        iceCream = new ChocolateChips(iceCream);
        Console.WriteLine(iceCream);
    }
}
