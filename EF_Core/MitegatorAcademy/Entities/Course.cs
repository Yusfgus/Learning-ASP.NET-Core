using System.Collections.Generic;

namespace EF_Core.MitegatorAcademy.Entities;

public class Course
{
    public int Id { get; set; }
    public string? CourseName { get; set; }
    public decimal Price { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();

    public override string ToString()
    {
        return $"{Id}, {CourseName}, {Price}";
    }
}
