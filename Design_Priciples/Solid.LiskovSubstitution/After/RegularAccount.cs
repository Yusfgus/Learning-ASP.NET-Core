namespace Design_Principles.Solid.LiskovSubstitution.After;

// Level 2
abstract class RegularAccount : Account
{
    public RegularAccount(string name, decimal balance)
        : base(name, balance)
    {
    }

    public abstract void Withdraw(decimal amount);
}

