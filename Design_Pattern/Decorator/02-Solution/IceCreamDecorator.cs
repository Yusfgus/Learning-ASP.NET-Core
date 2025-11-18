namespace Design_Pattern.Decorator.Solution;

// Decorator
public abstract class IceCreamDecorator : IceCream
{
    protected IceCream _iceCream;
    public IceCreamDecorator(IceCream iceCream)
    {
        _iceCream = iceCream;
    }

    public override string Description => _iceCream.Description;
    public override decimal CalculateCost() => _iceCream.CalculateCost();
}

// Concrete Decorator
public class Sprinkles : IceCreamDecorator
{
    public Sprinkles(IceCream iceCream) : base(iceCream)
    {
    }

    public override string Description => $"{base.Description} + Sprinkles";
    public override decimal CalculateCost() => base.CalculateCost() + 0.25m;
}

public class ChocolateChips : IceCreamDecorator
{
    public ChocolateChips(IceCream iceCream) : base(iceCream)
    {
    }

    public override string Description => $"{base.Description} + Chocolate Chips";
    public override decimal CalculateCost() => base.CalculateCost() + .45m;
}

public class FruitMix : IceCreamDecorator
{
    public FruitMix(IceCream iceCream) : base(iceCream)
    {
    }

    public override string Description => $"{base.Description} + Fruit Mix";
    public override decimal CalculateCost() => base.CalculateCost() + .60m;
}