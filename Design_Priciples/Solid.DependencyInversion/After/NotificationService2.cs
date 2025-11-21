
using Shared;

namespace Design_Principles.Solid.DependencyInversion.After;

internal class NotificationService2
{
    // property injection
    public INotificationMode[] _services {get; set;} = [];

    public void Notify()
    {
        Utils.printTitle("Property Injection", 50, System.ConsoleColor.Green);

        foreach(var x in _services)
            x.Send();
    }
}

