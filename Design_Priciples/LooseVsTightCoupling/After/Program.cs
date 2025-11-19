using System;
using Shared;

namespace Design_Principles.LooseVsTightCoupling.After;

class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Pattern", 80, ConsoleColor.Red);
        Utils.printTitle("Loose Vs Tight Coupling ( After )", 60, ConsoleColor.Blue);

        // NotificationService notificationService = new NotificationService(new EmailService());

        INotificationMode service = NotificationModeFactory.Create(NotificationMode.EMAIL);
        NotificationService notificationService = new NotificationService(service);

        notificationService.Notify();
    }
}
