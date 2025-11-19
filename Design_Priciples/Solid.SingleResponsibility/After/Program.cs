using System;
using Shared;

namespace Design_Principles.Solid.SingleResponsibility.After;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Solid: Single Responsibility ( After )", 60, ConsoleColor.Blue);

        var account = new Account("Reem", "reem@example.com", 10000m);

        var accountService = new AccountService();
        accountService.Deposit(account, 500);
        accountService.Withdraw(account, 11000);
    }
}
