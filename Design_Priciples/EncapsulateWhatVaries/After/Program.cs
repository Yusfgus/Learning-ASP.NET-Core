using System;
using System.Threading;
using Shared;

namespace Design_Principles.EncapsulateWhatVaries.After;

class Program
{
    static void Main(string[] args)
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Encapsulate What Varies ( After )", 70, ConsoleColor.Blue);

        Pizza pizza = Pizza.Order(PizzaType.Chicken);
        Console.WriteLine(pizza);
    }
}
