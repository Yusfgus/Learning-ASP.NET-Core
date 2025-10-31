namespace EF_Core.Migration.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Instructor(int id, string name, int officeId = 0)
    {
        this.Id = id;
        this.Name = name;
        // this.OfficeId = officeId;
    }
}