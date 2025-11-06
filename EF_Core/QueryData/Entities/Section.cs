using System;
using System.Collections.Generic;

namespace EF_Core.QueryData.Entities;

public class Section : Entity
{
    public string? SectionName { get; set; }
    public int CourseId { get; set; }
    public int? InstructorId { get; set; }
    public int ScheduleId { get; set; }
    public TimeSlot TimeSlot { get; set; } = null!;
    public DateRange DateRange { get; set; } = new();

    public Course Course { get; set; } = null!;
    public Instructor? Instructor { get; set; }
    public Schedule Schedule { get; set; } = null!;
    public ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public override string ToString()
    {
        return $"[{Id}] {SectionName}";
    }
}

public class TimeSlot
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public override string ToString()
    {
        return $"{StartTime:hh\\:mm} to {EndTime:hh\\:mm}";
    }
}

public class DateRange
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public override string ToString()
    {
        return $"{StartDate.ToString("yyyy-MM-dd")} - {EndDate.ToString("yyyy-MM-dd")}";
    }
}