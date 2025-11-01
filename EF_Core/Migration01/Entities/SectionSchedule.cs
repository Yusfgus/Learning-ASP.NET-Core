using System;

namespace EF_Core.Migration01.Entities;

public class SectionSchedule
{
    public int Id { get; set; }
    public int SectionId { get; set; }
    public int ScheduleId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public Section Section { get; set; } = null!;
    public Schedule Schedule { get; set; } = null!;
}