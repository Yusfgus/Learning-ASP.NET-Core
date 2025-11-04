using System;
using System.Collections.Generic;
using EF_Core.MitegatorAcademy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.MitegatorAcademy.Data.Config;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        
        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id).ValueGeneratedNever();

        // builder.Property(x => x.CourseName).HasMaxLength(255); // nvarchar(255)
        builder.Property(x => x.CourseName).HasColumnType("VARCHAR") // varchar(255)
                                            .HasMaxLength(255)
                                            .IsRequired();  // NOT NULL

        builder.Property(x => x.Price).HasPrecision(15, 2);

        // default data
        // builder.HasData(SeedData.LoadCourses());
    }

}
