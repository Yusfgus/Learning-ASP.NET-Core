using System.Collections.Generic;

namespace EF_Core.QueryData.Entities;

public class Course : Entity
{
    public string CourseName { get; set; } = null!;
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public override string ToString()
    {
        return $"[{Id}] {CourseName}, {Price}$, {HoursToComplete} Hours";
    }
}
