namespace DesignPrinciples.EncapsulateWhatVaries;

class VegetarianPizza : Pizza
{
    public override string Title => $"{base.Title} Vegetarian"; // Pizza Cheese
    public override decimal Price => base.Price + 4m;
}