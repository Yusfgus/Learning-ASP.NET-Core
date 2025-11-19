using System;
using System.Collections.Generic;

namespace Design_Principles.Solid.OpenClosed.After;

class MatchQuestion : Question
{
    public Dictionary<string, string> Rows { get; set; } = new Dictionary<string, string>();
    public override void Print()
    {
        base.Print();
        foreach ( var item in Rows)
        {
            Console.WriteLine($"{item.Key}            {item.Value}");
        }
    }
}
