
public class Employee : Person
{
    public float salary;

    public override string ToString()
    {
        return base.ToString() + $", salary: {salary}";
    }
}