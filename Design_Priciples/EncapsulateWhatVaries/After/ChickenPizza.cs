namespace Design_Principles.EncapsulateWhatVaries.After;

class ChickenPizza : Pizza
{
    public override string Title => $"{base.Title} Chicken"; // Pizza Cheese
    public override decimal Price => base.Price + 6m;
}
