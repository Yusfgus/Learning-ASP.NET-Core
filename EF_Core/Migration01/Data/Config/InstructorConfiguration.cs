using System;
using System.Collections.Generic;
using EF_Core.Migration01.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Migration01.Data.Config;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();

        // default data
        builder.HasData(LoadInstructors());
    }

    private static List<Instructor> LoadInstructors() => new List<Instructor>()
    {
        new Instructor(1, "Ahmed Abdullah"),
        new Instructor(2, "Yamen Mohammed"),
        new Instructor(3, "Khalid Hassan"),
        new Instructor(4, "Nadia Ali"),
        new Instructor(5, "Omar Ibrahim"),
    };
}