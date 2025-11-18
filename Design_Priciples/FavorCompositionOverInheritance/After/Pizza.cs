using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Design_Pattern.FavorCompositionOverInheritance.After;

class Pizza
{
    public string Title => $"{nameof(Pizza)}";
    public decimal Price => 10m;

    private List<ITopping> _toppings = new();

    public void AddTopping(ITopping topping) => _toppings.Add(topping);

    private decimal Calculate() =>  Price + _toppings.Sum(t => t.Price);

    public override string ToString()
    {
        StringBuilder output = new ($"\nPizza");
        output.Append($"\n\t{Title}: ({Price.ToString("C")})");
        foreach (var topping in _toppings)
        {
            output.Append($"\n\t{topping.Title} ({topping.Price.ToString("C")})");
        }
        output.Append("\n-----------------------");
        output.Append($"\nTotal: {Calculate().ToString("C")}");
        
        return output.ToString();
    }
}
