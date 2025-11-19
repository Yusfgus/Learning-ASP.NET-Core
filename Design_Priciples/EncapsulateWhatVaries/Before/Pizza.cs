using System;
using System.Threading;

namespace Design_Principles.EncapsulateWhatVaries.Before;

class Pizza
{
    public virtual string Title => $"{nameof(Pizza)}";
    public virtual decimal Price => 10m;

    public static Pizza Order(string type)
    {
        // changeable

        Pizza pizza;
        if (type.Equals("cheese"))
            pizza = new CheesePizza();
        else if (type.Equals("vegetarian"))
            pizza = new VegetarianPizza();
        else
            pizza = new ChickenPizza();


        // constant

        Prepare();
        Cook();
        Cut();

        return pizza;
    }

    private static void Prepare()
    {
        Console.Write("preparing...");
        Thread.Sleep(500);
        Console.WriteLine(" completed");
    }

    private static void Cook()
    {
        Console.Write("cooking...");
        Thread.Sleep(500);
        Console.WriteLine(" completed");
    }

    private static void Cut()
    {
        Console.Write("cutting and boxing...");
        Thread.Sleep(500);
        Console.WriteLine(" completed");
    }

    public override string ToString()
    {
        return $"\n{Title}" +
               $"\n\tPrice: {Price.ToString("C")}";
    }

}
