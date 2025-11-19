using System;
using Shared;

namespace Design_Principles.LooseVsTightCoupling.Before;

class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Pattern", 80, ConsoleColor.Red);
        Utils.printTitle("Loose Vs Tight Coupling ( Before )", 60, ConsoleColor.Blue);

        NotificationService notificationService = 
                    new NotificationService(new EmailService(), new SmsService());
        
        notificationService.Notify();
    }
}
