using System;

namespace Design_Principles.Solid.DependencyInversion.Before;

internal class SMSService
{
    public string MobileNo { get; set; }

    public void Send()
    {
        Console.WriteLine($"SMS is sent to {MobileNo}");
    }
}

