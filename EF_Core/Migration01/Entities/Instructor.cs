namespace EF_Core.Migration01.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string? FName { get; set; }
    public string? LName { get; set; }
    public int OfficeId { get; set; }
    public Office? office { get; set; }

    public Instructor(int id, string fName, string lName, int officeId)
    {
        this.Id = id;
        this.FName = fName;
        this.LName = lName;
        this.OfficeId = officeId;
    }
}
