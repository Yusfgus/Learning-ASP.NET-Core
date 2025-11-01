namespace EF_Core.Migration01.Entities;

public class Office
{
    public Office(int id, string officeName, string officeLocation)
    {
        this.Id = id;
        this.OfficeName = officeName;
        this.OfficeLocation = officeLocation;
    }

    public int Id { get; set; }
    public string? OfficeName { get; set; }
    public string? OfficeLocation { get; set; }
    public Instructor? instructor { get; set; }

    
}