using System;

namespace Design_Principles.Solid.LiskovSubstitution.Before;

class CheckingAccount : Account
{
    public CheckingAccount(string name, decimal balance)
        : base(name, balance)
    {
    }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount > 1000)
        {
            Console.WriteLine("You can't withdraw more than $1000");
            return;
        }
        Balance -= amount;
    }
}

