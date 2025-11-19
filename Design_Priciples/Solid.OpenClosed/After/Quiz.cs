using System;
using System.Collections.Generic;

namespace Design_Principles.Solid.OpenClosed.After;

class Quiz
{
    public List<Question> Questions { get; }

    public Quiz(List<Question> questions)
    {
        this.Questions = questions;
    }

    public void Print()
    {
        foreach (var q in Questions)
        {
            q.Print();
            Console.WriteLine("\n\n");
        }
    }
}
