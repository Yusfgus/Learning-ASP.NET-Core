using System.Collections.Generic;

namespace EF_Core.Migration01.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string? FName { get; set; }
    public string? LName { get; set; }
    public int OfficeId { get; set; }
    public Office? Office { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();

    public Instructor(int id, string fName, string lName, int officeId)
    {
        this.Id = id;
        this.FName = fName;
        this.LName = lName;
        this.OfficeId = officeId;
    }
}
