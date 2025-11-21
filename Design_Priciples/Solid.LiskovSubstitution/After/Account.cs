namespace Design_Principles.Solid.LiskovSubstitution.After;


// Level 1
abstract class Account
{
    protected Account(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }

    public string Name { get; set; }
    public decimal Balance { get; set; }

    public abstract void Deposit(decimal amount);
}
