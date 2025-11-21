using System;

namespace Design_Principles.Solid.LiskovSubstitution.Before;

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

    public override void Withdraw(decimal amount)
    {
        throw new NotSupportedException($"You can't withdraw from Fixed Deposit Account!!!");
    }
}

