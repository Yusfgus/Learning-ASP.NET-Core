namespace EF_Core.MitegatorAcademy.Entities;

public class Enrollment
{
    public int SectionId { get; set; }   
    public int StudentId { get; set; }

    public Section Section { get; set; } = null!;
    public Student Student { get; set; } = null!;
}
