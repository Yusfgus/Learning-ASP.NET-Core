using System;

namespace Design_Principles.LooseVsTightCoupling.After;

class SmsService : INotificationMode
{
    public void Send()
    {
        Console.WriteLine("sms sent");
    }
}
