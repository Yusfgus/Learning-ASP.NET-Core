using System;
using System.Threading;

namespace DesignPrinciples.EncapsulateWhatVaries;

class Program
{
    static void Main(string[] args)
    {
        Pizza pizza = Pizza.Order("chicken");
        Console.WriteLine(pizza);
    }
}
