using System;
using System.Collections.Generic;
using EF_Core.MitegatorAcademy.Entities;
using EF_Core.MitegatorAcademy.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.MitegatorAcademy.Data.Config;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");

        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Title)
                .HasConversion(
                    x => x.ToString(), // when saving in database
                    v => (ScheduleTitleEnum)Enum.Parse(typeof(ScheduleTitleEnum), v)  // when reading from database
                );


        // default data
        // builder.HasData(SeedData.LoadSchedules());
    }
}
