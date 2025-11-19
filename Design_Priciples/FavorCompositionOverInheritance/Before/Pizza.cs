namespace Design_Principles.FavorCompositionOverInheritance.Before;

class Pizza
{
    public virtual string Title => $"{nameof(Pizza)}";
    public virtual decimal Price => 10m;

    public static Pizza Create(int choice)
    {
        Pizza pizza = null;
        switch (choice)
        {
            case 1:
                pizza = new ChickenPizza();
                break;
            case 2:
                pizza = new VegetarianPizza();
                break;
            case 3:
                pizza = new MexicanPizza();
                break;
            default:
                break;
        }
        return pizza;
    }

    public override string ToString()
    {
        return $"\n{Title}" +
               $"\n\tPrice: {Price.ToString("C")}";
    }
}
