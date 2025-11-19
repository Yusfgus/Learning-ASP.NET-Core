using System;

namespace Design_Principles.Solid.SingleResponsibility.After;

public class EmailClient
{
    public static void Send(Account account, string message, DateTime date)
    {
        Console.WriteLine(
         $"\n\n\t\t To: {account.Email}" +
         $"\n\t\t Subject: Fake Bank Account Activity" +
         $"\n\n\t\t Dear {account.Name}," +
         $"\n\n\t\t\t A recent activity on your account occurs at {date.ToString("yyyy-MM-dd hh:mm")}" +
         "\n\t\t\t\t ===> {0}" +
         $"\n\n\t\t Thank You,\n\t\t Fake Bank." +
         $"\n\n\t\t--------------------------- ", message);
    }
}
