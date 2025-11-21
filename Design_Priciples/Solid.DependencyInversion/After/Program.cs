using Shared;

namespace Design_Principles.Solid.DependencyInversion.After;

class Program
{
    static void Main()
    {
        Utils.printTitle("Design Principles", 80, System.ConsoleColor.Red);
        Utils.printTitle("Dependency Inversion ( After )", 60, System.ConsoleColor.Blue);

        var customers = Repository.Customers;

        foreach (var customer in customers)
        {
            INotificationMode[] services =
            [
                new EmailService { EmailAddress = customer.EmailAddress },
                new SMSService { MobileNo = customer.MobileNo },
                new MailService { Address = customer.Address }
            ];

            // constructor injection
            var notificationService1 = new NotificationService1(services);
            notificationService1.Notify();

            // // property injection
            // var notificationService2 = new NotificationService2();
            // notificationService2._services = services;
            // notificationService2.Notify();

            // // method injection
            // var notificationService3 = new NotificationService3();
            // notificationService3.Notify(services);

            System.Console.WriteLine("---------------------------------------------------");
        }
    }
}
