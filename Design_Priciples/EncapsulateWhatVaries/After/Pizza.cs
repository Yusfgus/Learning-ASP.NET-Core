using System;
using System.Threading;

namespace Design_Principles.EncapsulateWhatVaries.After;

class Pizza
{
    public virtual string Title => $"{nameof(Pizza)}";
    public virtual decimal Price => 10m;

    public static Pizza Create(PizzaType type)
    {
        Pizza pizza;
        if (type.Equals(PizzaType.Cheese))
            pizza = new CheesePizza();
        else if (type.Equals(PizzaType.Vegetarian))
            pizza = new VegetarianPizza();
        else
            pizza = new ChickenPizza();
        
        return pizza;
    }

    public static Pizza Order(PizzaType type)
    {
        Pizza pizza = Create(type);

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
