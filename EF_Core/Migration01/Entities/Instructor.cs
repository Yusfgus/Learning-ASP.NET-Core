namespace EF_Core.Migration01.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Instructor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        // this.OfficeId = officeId;
    }
}