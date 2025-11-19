using System;
using System.Collections.Generic;

namespace Design_Principles.Solid.OpenClosed.After;

class MultipleChoiceQuestion : Question
{
    public List<string> Choices { get; set; } = new List<string>();

    public override void Print()
    {
        base.Print();
        foreach (var choice in Choices)
        {
            Console.WriteLine($"  {choice}");
        }
    }
}