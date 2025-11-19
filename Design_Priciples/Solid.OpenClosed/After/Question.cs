using System;

namespace Design_Principles.Solid.OpenClosed.After;

abstract class Question
{
    public string Title { get; set; }
    public int Mark { get; set; }

    public virtual void Print()
    {
        Console.WriteLine($"{Title} [{Mark}]");
    }
}
