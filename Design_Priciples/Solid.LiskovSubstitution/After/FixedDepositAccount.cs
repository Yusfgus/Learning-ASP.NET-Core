using System;

namespace Design_Principles.Solid.LiskovSubstitution.After;

// Level 2
class FixedDepositAccount : Account
{
    public FixedDepositAccount(string name, decimal balance)
        : base(name, balance)
    {
    }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
    }
}

