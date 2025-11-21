using System;

namespace Design_Principles.Solid.DependencyInversion.After;

internal class EmailService : INotificationMode
{
    public string EmailAddress { get; set; }

    public void Send()
    {
        Console.WriteLine($"e-mail is sent to {EmailAddress}");
    }
}
