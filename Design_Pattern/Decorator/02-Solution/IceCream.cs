namespace Design_Pattern.Decorator.Solution;

// Component
public abstract class IceCream
{
    public abstract string Description {get;}
    public abstract decimal CalculateCost();

    public override string ToString()
    {
        return $"{Description} ({CalculateCost().ToString("C")})";
    }
}

// Concrete Component
public class BasicIceCream : IceCream
{
    public override string Description => "Ice Cream";

    public override decimal CalculateCost() => 3.5m;
}
