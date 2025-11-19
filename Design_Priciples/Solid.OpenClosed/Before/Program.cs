using System;
using Shared;

namespace Design_Principles.Solid.OpenClosed.Before;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Solid: Open Closed ( Before )", 60, ConsoleColor.Blue);

        var quiz = new Quiz(QuestionBank.Generate());
        quiz.Print();

        // Problem
        // In order to add new question type we have to modify
        // 1- Quiz switch case
        // 2- QuestionType enum
    }
}
