using System.Collections.Generic;
using EF_Core.Migration01.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Migration01.Data.Config;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");

        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id).ValueGeneratedNever();

        // builder.Property(x => x.CourseName).HasMaxLength(100); // nvarchar(100)
        builder.Property(x => x.Title)
                .HasColumnType("VARCHAR") // varchar(100)
                .HasMaxLength(100)
                .IsRequired();  // NOT NULL


        // default data
        // builder.HasData(LoadSchedules());
    }

    // private static List<Schedule> LoadSchedules() => new List<Schedule>()
    // {
    //     new Schedule { Id = 1, Title = "Daily", SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = false, SAT = false },
    //     new Schedule { Id = 2, Title = "DayAfterDay", SUN = true, MON = false, TUE = true, WED = false, THU = true, FRI = false, SAT = false },
    //     new Schedule { Id = 3, Title = "Twice-a-Week", SUN = false, MON = true, TUE = false, WED = true, THU = false, FRI = false, SAT = false },
    //     new Schedule { Id = 4, Title = "Weekend", SUN = false, MON = false, TUE = false, WED = false, THU = false, FRI = true, SAT = true },
    //     new Schedule { Id = 5, Title = "Compact", SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = true, SAT = true }
    // };
}
