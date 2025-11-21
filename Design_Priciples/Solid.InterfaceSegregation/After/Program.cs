using Shared;
using System;

namespace Design_Principles.Solid.InterfaceSegregation.After;

class Program
{
    static void Main()
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Interface Segregation ( After )", 60, ConsoleColor.Blue);

        var employees = Repository.LoadEmployees();

        foreach (var e in employees)
        {
            Console.WriteLine(e.PrintSalarySlip());
            Console.WriteLine();
        }
    }
}