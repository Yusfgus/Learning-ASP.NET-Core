using System;
using Shared;

namespace Design_Principles.Solid.OpenClosed.After;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Solid: Open Closed ( After )", 60, ConsoleColor.Blue);

        var quiz = new Quiz(QuestionBank.Generate());
        quiz.Print();
    }
}
