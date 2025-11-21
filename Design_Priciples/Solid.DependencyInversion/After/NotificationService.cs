
using Shared;

namespace Design_Principles.Solid.DependencyInversion.After;

internal class NotificationService1
{
    public readonly INotificationMode[] _notificationMode;

    // constructor injection
    public NotificationService1(INotificationMode[] notificationMode)
    {
        _notificationMode = notificationMode;   
    }

    public void Notify()
    {
        Utils.printTitle("Constructor Injection", 50, System.ConsoleColor.Green);

        foreach(var x in _notificationMode)
            x.Send();
    }
}
