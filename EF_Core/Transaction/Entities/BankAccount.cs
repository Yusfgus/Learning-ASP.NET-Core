using System;
using System.Collections.Generic;
using EF_Core.Transactions.Data;

namespace EF_Core.Transactions.Entities;

public class BankAccount
{
    public string AccountId { get; set; }
    public string AccountHolder { get; set; }
    public decimal CurrentBalance { get; set; }

    public List<GLTransaction> GLTransactions { get; set; } = new();

    public void Deposit(decimal amount)
    {
        if (amount < 0)
            throw new Exception($"Amount can't be negative, {amount} was passed");

        CurrentBalance += amount;
        GLTransactions.Add(new GLTransaction(amount, "DEPOSIT", DateTime.Now));

        Console.WriteLine("Deposit completed successfully");
    }

    public void WithDraw(decimal amount)
    {
        if (amount < 0)
            throw new Exception($"Amount can't be negative, {amount} was passed");

        if (CurrentBalance < amount)
            throw new Exception($"WithDraw amount ({amount}) is greater than the CurrentBalance ({CurrentBalance})");

        CurrentBalance -= amount;
        GLTransactions.Add(new GLTransaction(-amount, "WITHDRAW", DateTime.Now));

        Console.WriteLine("WithDraw completed successfully");
    }

    public override string ToString()
    {
        return $"[{AccountId}] {AccountHolder}, {CurrentBalance}$";
    }
}
