using System;

namespace Design_Principles.LooseVsTightCoupling.After;

class EmailService : INotificationMode
{
    public void Send()
    {
        Console.WriteLine("email sent");
    }
}
