
public class Student : Person
{
    public string seat_number = "";

    public override string ToString()
    {
        return base.ToString() + $", seat number: {seat_number}";
    }
}
