using System;
using System.Threading;

namespace Design_Pattern.FavorCompositionOverInheritance.Before;

class Program
{
    static void Main(string[] args)
    {
        int len = Enum.GetNames(typeof(PizzaType)).Length;
        int choice;
        while(true)
        {
            Console.Clear();
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
