

public class Equality
{
    private class Person
    {
        public int age;
        public string name="";
    }

    private struct Fruit
    {
        public int price;
        public string name;
    }

    public static void Equals()
    {
        Utils.printTitle(title: "Equality ( Equals )", color: ConsoleColor.Blue, width: 70);

        int num1 = 1, num2 = 1;
        bool isEqual01 = num1.Equals(num2);
        
        Utils.printTitle("Int type");
        Console.WriteLine($"num1 and num2 {(isEqual01 ? "are" : "aren't")} equal");

        // ====================================================================

        string name1 = "apple", name2 = "apple";
        bool isEqual02 = name1.Equals(name2);

        Utils.printTitle("String type");
        Console.WriteLine($"name1 and name2 {(isEqual02 ? "are" : "aren't")} equal");

        // ====================================================================

        Person p1 = new Person { age=20, name="yousef"};
        Person p2 = new Person { age=20, name="yousef"};
        bool isEqual03 = p1.Equals(p2);

        Utils.printTitle("Class type (without Equals() override)");
        Console.WriteLine($"p1 and p1 {(isEqual03 ? "are" : "aren't")} equal");

        // ====================================================================

        Fruit f1 = new Fruit { price=25, name="banana"};
        Fruit f2 = new Fruit { price=25, name="banana"};
        bool isEqual04 = f1.Equals(f2);

        Utils.printTitle("Struct type (without Equals() override)");
        Console.WriteLine($"f1 and f1 {(isEqual04 ? "are" : "aren't")} equal");
    }
    
    public static void SequenceEqual()
    {
        Utils.printTitle(title: "Equality ( SequenceEqual )", color: ConsoleColor.Blue, width: 70);

        int[] numbers01 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> numbers02 = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        bool isEqual01 = numbers01.SequenceEqual(numbers02);

        Utils.printTitle("Sequence of int type");
        Console.WriteLine($"numbers01 and numbers02{(isEqual01 ? "are" : "aren't")} equal"); 

        // ===================================================================================================================
        
        string[] names01 = { "name1", "name2", "name3", "name4", "name5" };
        List<string> names02 = new() { "name1", "name2", "name3", "name4", "name5" };
        bool isEqual02 = names01.SequenceEqual(names02);

        Utils.printTitle("Sequence of string type");
        Console.WriteLine($"names01 and names02 {(isEqual02 ? "are" : "aren't")} equal"); 
        
        // ===================================================================================================================

        Person[] persons01 = { new Person { age = 20, name = "yousef" }, new Person { age = 21, name = "ahmed" } };
        List<Person> persons02 = new() { new Person { age = 20, name = "yousef" }, new Person { age = 21, name = "ahmed" } };
        bool isEqual03 = persons01.SequenceEqual(persons02);

        Utils.printTitle("Sequence of Class type (without Equals() override)");
        Console.WriteLine($"p1 and p1 {(isEqual03 ? "are" : "aren't")} equal");

        // ===================================================================================================================

        Fruit[] fruits01 = { new Fruit { price = 25, name = "banana" }, new Fruit { price = 30, name = "apple" } };
        List<Fruit> fruits02 = new() { new Fruit { price = 25, name = "banana" }, new Fruit { price = 30, name = "apple" } };
        bool isEqual04 = fruits01.SequenceEqual(fruits02);

        Utils.printTitle("Sequence of Struct type (without Equals() override)");
        Console.WriteLine($"f1 and f1 {(isEqual04 ? "are" : "aren't")} equal");
    }
}