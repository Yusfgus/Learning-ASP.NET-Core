using System;
using Shared;

namespace Design_Principles.Solid.LiskovSubstitution.After;

class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Liskov Substitution ( Before )", 70, ConsoleColor.Blue);

        // var account = new FixedDepositAccount("Issam", 10_000);
        // account.Withdraw(1000);  // compiler error

        var account = new SavingAccount("Issam", 10_000);
        account.Withdraw(1000);
    }
}