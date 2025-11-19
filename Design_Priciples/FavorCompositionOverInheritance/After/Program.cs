using System;
using Shared;
using System.Threading;

namespace Design_Principles.FavorCompositionOverInheritance.After;

class Program
{
    static void Main(string[] args)
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Favor Composition Over Inheritance ( After )", 70, ConsoleColor.Blue);

        Pizza pizza = new Pizza();
        int choice;

        while (true)
        {
            choice = ReadChoice();
            ITopping topping = choice switch
            {
                1 => new Tomato(),
                2 => new Chicken(),
                3 => new Cheese(),
                4 => new BlackOlive(),
                5 => new GreenPaper(),
                6 => new Salami(),
                _ => null
            };

            if(topping is not null)
                pizza.AddTopping(topping);
            else
                break;
        }

        Console.WriteLine(pizza);
    }

    private static int ReadChoice()
    {
        Console.WriteLine("\nAvailable Topping");
        Console.WriteLine("------------");
        Console.WriteLine("1. Tomato");
        Console.WriteLine("2. Chicken");
        Console.WriteLine("3. Cheese");
        Console.WriteLine("4. Black Olives");
        Console.WriteLine("5. Green Paper");
        Console.WriteLine("6. Salami");
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
