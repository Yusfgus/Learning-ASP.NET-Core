
public class Person
{
    public int id;
    public int age;
    public string name = "";

    public override string ToString()
    {
        return $"id: {id}, age: {age}, name: {name}";
    }
}