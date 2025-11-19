using System;

namespace Design_Pattern.LooseVsTightCoupling.Before;

class SmsService
{
    public void Send()
    {
        Console.WriteLine("sms sent");
    }
}
