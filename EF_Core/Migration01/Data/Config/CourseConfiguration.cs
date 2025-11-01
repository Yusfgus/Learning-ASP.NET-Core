using System;
using System.Collections.Generic;
using EF_Core.Migration01.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Migration01.Data.Config;

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
        builder.HasData(LoadCourses());
    }

    private static List<Course> LoadCourses() => new List<Course>()
    {
        new Course {Id = 1, CourseName = "Mathematics", Price = 2000m},
        new Course {Id = 2, CourseName = "Physics", Price = 2000m},
        new Course {Id = 3, CourseName = "Chemistry", Price = 1500m},
        new Course {Id = 4, CourseName = "Biology", Price = 1200m},
        new Course {Id = 5, CourseName = "CS-50", Price = 3000m},
    };
}
