using System;
using System.Collections.Generic;

namespace EF_Core.Migration01.Entities;

public class TimeSlot
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public override string ToString()
    {
        return $"{StartTime:hh\\:mm} to {EndTime:hh\\:mm}";
    }
}

public class Section
{
    public int Id { get; set; }
    public string? SectionName { get; set; }
    public int CourseId { get; set; }
    public int? InstructorId { get; set; }
    public int ScheduleId { get; set; }
    public TimeSlot TimeSlot { get; set; } = null!;

    public Course Course { get; set; } = null!;

    public Instructor? Instructor { get; set; }
    
    public Schedule Schedule { get; set; } = null!;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
