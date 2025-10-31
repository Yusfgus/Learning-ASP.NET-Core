namespace EF_Core.Migration01.Entities;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
    public decimal Price { get; set; }
}
