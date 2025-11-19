namespace Design_Principles.FavorCompositionOverInheritance.Before;

class MexicanPizza : Pizza
{
    public override string Title => $"{base.Title} Mexican"; // Pizza Cheese
    public override decimal Price => base.Price + 3m;
}
