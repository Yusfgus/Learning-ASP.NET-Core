using System;
using Shared;

namespace Design_Principles.Solid.SingleResponsibility.Before;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Solid: Single Responsibility ( Before )", 60, ConsoleColor.Blue);

        var account = new Account("Reem", "reem@example.com", 10000m);
        account.MakeTransaction(500);
        account.MakeTransaction(-11000);
    }
}
