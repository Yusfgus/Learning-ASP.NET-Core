using System;

namespace Design_Pattern.LooseVsTightCoupling.After;

class SmsService : INotificationMode
{
    public void Send()
    {
        Console.WriteLine("sms sent");
    }
}
