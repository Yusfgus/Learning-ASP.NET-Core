using System.Collections.Generic;

namespace EF_Core.Migration01.Entities;

public class Section
{
    public int Id { get; set; }
    public string? SectionName { get; set; }
    public int CourseId { get; set; }
    public int? InstructorId { get; set; }


    public Course Course { get; set; } = null!;

    public Instructor? Instructor { get; set; }
    
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public ICollection<SectionSchedule> SectionSchedules { get; set; } = new List<SectionSchedule>();

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
