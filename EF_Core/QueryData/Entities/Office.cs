namespace EF_Core.QueryData.Entities;

public class Office : Entity
{
    public string? OfficeName { get; set; }
    public string? OfficeLocation { get; set; }
    public Instructor? Instructor { get; set; }

    public Office(int id, string officeName, string officeLocation)
    {
        this.Id = id;
        this.OfficeName = officeName;
        this.OfficeLocation = officeLocation;
    }
}