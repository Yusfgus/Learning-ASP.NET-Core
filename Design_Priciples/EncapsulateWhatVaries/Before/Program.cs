using System;
using System.Threading;
using Shared;

namespace Design_Principles.EncapsulateWhatVaries.Before;

class Program
{
    
    static void Main(string[] args)
    {
        Utils.printTitle("Design Principles", 80, ConsoleColor.Red);
        Utils.printTitle("Encapsulate What Varies ( Before ) ", 70, ConsoleColor.Blue);

        Pizza pizza = Pizza.Order("chicken");
        Console.WriteLine(pizza);
    }
}
