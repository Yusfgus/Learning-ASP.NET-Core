using System;

namespace Design_Pattern.LooseVsTightCoupling.After;

class EmailService : INotificationMode
{
    public void Send()
    {
        Console.WriteLine("email sent");
    }
}
