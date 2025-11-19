using System;

namespace Design_Principles.LooseVsTightCoupling.Before;

class SmsService
{
    public void Send()
    {
        Console.WriteLine("sms sent");
    }
}
