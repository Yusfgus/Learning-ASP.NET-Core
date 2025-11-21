using Shared;
using System;

namespace Design_Principles.Solid.InterfaceSegregation.Before;

class Program
{
    static void Main()
    {
        Utils.printTitle("Design Principles", 80, System.ConsoleColor.Red);
        Utils.printTitle("Interface Segregation ( Before )", 60, System.ConsoleColor.Blue);

        var employees = Repository.LoadEmployees();

        foreach (var e in employees)
        {
            Console.WriteLine(e.PrintSalarySlip());
            Console.WriteLine();
        }
    }
}