using System;
using Shared;
using System.Threading;

namespace Design_Principles.FavorCompositionOverInheritance.Before;

class Program
{
    static void Main(string[] args)
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Favor Composition Over Inheritance ( Before )", 70, ConsoleColor.Blue);

        int len = Enum.GetNames(typeof(PizzaType)).Length;
        int choice;
        while(true)
        {
            choice = ReadChoice();
            if (choice >= 1 && choice <= len)
            {
                var pizza = Pizza.Create(choice);
                Console.WriteLine(pizza);
                Console.Write("\nPress any key to continue...");
                Console.ReadKey();
            }
            else 
                break;
        }

        // Limitation
        // can't make a chicken with cheese pizza :(
        // unless we create class ChickenWithCheesePizza :<
    }

    private static int ReadChoice()
    {
        Console.WriteLine("Today's Menu");
        Console.WriteLine("------------");
        Console.WriteLine("1. Chicken");
        Console.WriteLine("2. Vegetarian");
        Console.WriteLine("3. Mexican");
        Console.WriteLine("or any key to exit");
        Console.Write("-->: ");
        if (int.TryParse(Console.ReadLine(), out int ch))
        {
            return ch;
        }
        else 
            return 0;
    }
}
