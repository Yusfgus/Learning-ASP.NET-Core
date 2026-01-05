namespace BasicSetup.Models;

public class Product
{
    public int Id {get; set;}
    public string Name {get; set;} = null!;
    public decimal Price {get; set;}
    public int Amount {get; set;}

    public override string ToString()
    {
        return $"{Id} | {Name} | {Price} | {Amount}";
    }
}