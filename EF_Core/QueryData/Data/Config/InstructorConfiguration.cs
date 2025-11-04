using System;
using System.Collections.Generic;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.QueryData.Data.Config;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.FName).HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();
        builder.Property(x => x.LName).HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();

        builder.HasOne(x => x.Office)  // in instructor
                .WithOne(x => x.Instructor) // in office
                .HasForeignKey<Instructor>(x => x.OfficeId) // in instructor
                .IsRequired(false);

        // default data
        // builder.HasData(SeedData.LoadInstructors());
    }
}