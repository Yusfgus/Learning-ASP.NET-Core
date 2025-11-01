using System.Collections.Generic;
using EF_Core.Migration01.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Migration01.Data.Config;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");

        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id).ValueGeneratedNever();

        // builder.Property(x => x.CourseName).HasMaxLength(255); // nvarchar(255)
        builder.Property(x => x.SectionName).HasColumnType("VARCHAR") // varchar(255)
                                            .HasMaxLength(255)
                                            .IsRequired();  // NOT NULL

        // One-to-Many
        builder.HasOne(x => x.Course)  // in section
                .WithMany(x => x.Sections) // in course
                .HasForeignKey(x => x.CourseId) // in section
                .IsRequired();  // required (section can't exist without course)

        // One-to-Many
        builder.HasOne(x => x.Instructor)  // in section
                .WithMany(x => x.Sections) // in instructor
                .HasForeignKey(x => x.InstructorId) // in section
                .IsRequired(false); // optional (section can exist without instructor)


        // Many-to-Many
        builder.HasMany(x => x.Schedules)
                .WithMany(x => x.Sections)
                .UsingEntity<SectionSchedule>();  // Join table


        // default data
        builder.HasData(LoadSections());
    }

    private static List<Section> LoadSections() => new List<Section>()
    {
        new Section(){Id = 1, SectionName = "S_MA1", CourseId = 1, InstructorId = 1},
        new Section(){Id = 2, SectionName = "S_MA2", CourseId = 1, InstructorId = 2},
        new Section(){Id = 3, SectionName = "S_PH1", CourseId = 2, InstructorId = 1},
        new Section(){Id = 4, SectionName = "S_PH2", CourseId = 2, InstructorId = 3},
        new Section(){Id = 5, SectionName = "S_CH1", CourseId = 3, InstructorId = 2},
        new Section(){Id = 6, SectionName = "S_CH2", CourseId = 3, InstructorId = 3},
        new Section(){Id = 7, SectionName = "S_BI1", CourseId = 4, InstructorId = 4},
        new Section(){Id = 8, SectionName = "S_BI2", CourseId = 4, InstructorId = 5},
        new Section(){Id = 9, SectionName = "S_CS1", CourseId = 5, InstructorId = 4},
        new Section(){Id = 10,SectionName = "S_CS2", CourseId = 5, InstructorId = 5},
        new Section(){Id = 11,SectionName = "S_CS3", CourseId = 5, InstructorId = 4},
    };
}
