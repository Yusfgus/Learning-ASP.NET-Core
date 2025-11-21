using System;
using Shared;

namespace Design_Principles.Solid.LiskovSubstitution.Before;

class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Liskov Substitution ( Before )", 70, ConsoleColor.Blue);

        var account = new FixedDepositAccount("Issam", 10_000);   // will throw an exception
        account.Withdraw(1000); 

        // Problem is
        // class FixedDepositAccount must implement Withdraw()
        // but logically it shouldn't
    }
}