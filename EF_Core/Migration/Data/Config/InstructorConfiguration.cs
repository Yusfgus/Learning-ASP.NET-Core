using System;
using System.Collections.Generic;
using EF_Core.Migration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Migration.Data.Config;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(50);

        // default data
        builder.HasData(LoadInstructors());
    }

    private static List<Instructor> LoadInstructors() => new List<Instructor>()
    {
        new Instructor(1, "Ahmed Abdullah", 1),
        new Instructor(2, "Yamen Mohammed", 2),
        new Instructor(3, "Khalid Hassan", 3),
        new Instructor(4, "Nadia Ali", 4),
        new Instructor(5, "Omar Ibrahim", 5),
    };
}