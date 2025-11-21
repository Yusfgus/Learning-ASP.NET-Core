using System;

namespace Design_Principles.Solid.DependencyInversion.After;

internal class SMSService : INotificationMode
{
    public string MobileNo { get; set; }

    public void Send()
    {
        Console.WriteLine($"SMS is sent to {MobileNo}");
    }
}

