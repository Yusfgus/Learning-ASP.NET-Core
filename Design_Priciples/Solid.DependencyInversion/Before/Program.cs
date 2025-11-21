using Shared;

namespace Design_Principles.Solid.DependencyInversion.Before;

class Program
{
    static void Main()
    {
        Utils.printTitle("Design Principles", 80, System.ConsoleColor.Red);
        Utils.printTitle("Dependency Inversion ( Before )", 60, System.ConsoleColor.Blue);

        var customers = Repository.Customers;

        foreach (var customer in customers)
        {
            var notificationService = new NotificationService(customer);
            notificationService.Notify();
        }
    }
}