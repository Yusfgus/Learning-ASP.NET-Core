namespace DesignPrinciples.EncapsulateWhatVaries;

class CheesePizza : Pizza
{
    public override string Title => $"{base.Title} Cheese"; // Pizza Cheese
    public override decimal Price => base.Price + 3m;
}
