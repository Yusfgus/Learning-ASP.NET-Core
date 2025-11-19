using System;

namespace Design_Principles.Solid.OpenClosed.After;

class WHQuestion : Question
{
    public override void Print()
    {
        base.Print();
        Console.WriteLine("  _____________________________");
        Console.WriteLine("  _____________________________");
        Console.WriteLine("  _____________________________");
    }
}
