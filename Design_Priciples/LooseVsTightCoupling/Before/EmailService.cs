using System;

namespace Design_Principles.LooseVsTightCoupling.Before;

class EmailService
{
    public void Send()
    {
        Console.WriteLine("email sent");
    }
}
