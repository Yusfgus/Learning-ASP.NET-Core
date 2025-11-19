using System;

namespace Design_Pattern.LooseVsTightCoupling.Before;

class EmailService
{
    public void Send()
    {
        Console.WriteLine("email sent");
    }
}
