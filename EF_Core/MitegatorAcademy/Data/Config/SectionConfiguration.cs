using System;
using System.Collections.Generic;
using EF_Core.MitegatorAcademy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.MitegatorAcademy.Data.Config;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
	public void Configure(EntityTypeBuilder<Section> builder)
	{
		builder.ToTable("Sections");

		builder.HasKey(x => x.Id);  // set as primary key
		builder.Property(x => x.Id).ValueGeneratedNever();

		// builder.Property(x => x.CourseName).HasMaxLength(255); // nvarchar(255)
		builder.Property(x => x.SectionName)
				.HasColumnType("VARCHAR") // varchar(255)
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

		// // Many-to-Many
		// builder.HasMany(x => x.Schedules)
		//         .WithMany(x => x.Sections)
		//         .UsingEntity<SectionSchedule>();  // Join table

		// One-to-Many
		builder.HasOne(x => x.Schedule)
				.WithMany(x => x.Sections)
				.HasForeignKey(x => x.ScheduleId)
				.IsRequired();

		builder.OwnsOne(x => x.TimeSlot, ts =>
		{
			ts.Property(p => p.StartTime).HasColumnType("time").HasColumnName("StartTime");
			ts.Property(p => p.EndTime).HasColumnType("time").HasColumnName("EndTime");
		});

		// Many-to-Many
		builder.HasMany(x => x.Participants)
				.WithMany(x => x.Sections)
				.UsingEntity<Enrollment>();  // Join table


		// default data
		// builder.HasData(SeedData.LoadSections());
	}
}
