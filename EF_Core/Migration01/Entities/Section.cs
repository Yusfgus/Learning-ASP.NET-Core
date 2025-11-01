namespace EF_Core.Migration01.Entities;

public class Section
{
    public int Id { get; set; }
    public string? SectionName { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public int? InstructorId { get; set; }
    public Instructor? Instructor { get; set; }
}
