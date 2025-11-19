namespace Design_Principles.FavorCompositionOverInheritance.Before;

class ChickenPizza : Pizza
{
    public override string Title => $"{base.Title} Chicken"; // Pizza Cheese
    public override decimal Price => base.Price + 6m;
}
