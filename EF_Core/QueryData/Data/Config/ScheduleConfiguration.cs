using System;
using EF_Core.QueryData.Entities;
using EF_Core.QueryData.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.QueryData.Data.Config;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");

        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ScheduleType)
                .HasConversion(
                    x => x.ToString(), // when saving in database
                    v => (ScheduleTypeEnum)Enum.Parse(typeof(ScheduleTypeEnum), v)  // when reading from database
                );


        // default data
        // builder.HasData(SeedData.LoadSchedules());
    }
}
