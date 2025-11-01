using System;
using System.Collections.Generic;
using EF_Core.Migration01.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Migration01.Data.Config;

public class SectionScheduleConfiguration : IEntityTypeConfiguration<SectionSchedule>
{
    public void Configure(EntityTypeBuilder<SectionSchedule> builder)
    {
        builder.ToTable("SectionSchedules");

        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.StartTime).HasColumnType("time");
        builder.Property(x => x.EndTime).HasColumnType("time");

        // default data
        builder.HasData(LoadSectionSchedules());
    }

    private static List<SectionSchedule> LoadSectionSchedules() => new List<SectionSchedule>()
    {
        new SectionSchedule { Id = 1, SectionId = 1, ScheduleId = 1, StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00") },
        new SectionSchedule { Id = 2, SectionId = 2, ScheduleId = 3, StartTime = TimeSpan.Parse("14:00:00"), EndTime = TimeSpan.Parse("18:00:00") },
        new SectionSchedule { Id = 3, SectionId = 3, ScheduleId = 4, StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("15:00:00") },
        new SectionSchedule { Id = 4, SectionId = 4, ScheduleId = 1, StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("12:00:00") },
        new SectionSchedule { Id = 5, SectionId = 5, ScheduleId = 1, StartTime = TimeSpan.Parse("16:00:00"), EndTime = TimeSpan.Parse("18:00:00") },
        new SectionSchedule { Id = 6, SectionId = 6, ScheduleId = 2, StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00") },
        new SectionSchedule { Id = 7, SectionId = 7, ScheduleId = 3, StartTime = TimeSpan.Parse("11:00:00"), EndTime = TimeSpan.Parse("14:00:00") },
        new SectionSchedule { Id = 8, SectionId = 8, ScheduleId = 4, StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("14:00:00") },
        new SectionSchedule { Id = 9, SectionId = 9, ScheduleId = 4, StartTime = TimeSpan.Parse("16:00:00"), EndTime = TimeSpan.Parse("18:00:00") },
        new SectionSchedule { Id = 10, SectionId = 10, ScheduleId = 3, StartTime = TimeSpan.Parse("12:00:00"), EndTime = TimeSpan.Parse("15:00:00") },
        new SectionSchedule { Id = 11, SectionId = 11, ScheduleId = 5, StartTime = TimeSpan.Parse("09:00:00"), EndTime = TimeSpan.Parse("11:00:00") }
    };
}
