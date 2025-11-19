using System;

namespace Design_Principles.Solid.OpenClosed.After;

class TrueFalseQuestion : Question
{
    public override void Print()
    {
        base.Print();
        Console.WriteLine("  1. T");
        Console.WriteLine("  2. F");
    }
}
