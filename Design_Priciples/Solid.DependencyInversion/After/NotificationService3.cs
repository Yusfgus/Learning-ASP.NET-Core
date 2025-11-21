
using Shared;

namespace Design_Principles.Solid.DependencyInversion.After;

internal class NotificationService3
{
    // method injection
    public void Notify(INotificationMode[] services)
    {
        Utils.printTitle("Method Injection", 50, System.ConsoleColor.Green);

        foreach(var x in services)
            x.Send();
    }
}

