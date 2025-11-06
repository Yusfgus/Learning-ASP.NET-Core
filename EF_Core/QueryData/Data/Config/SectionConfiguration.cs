using System;
using System.Collections.Generic;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.QueryData.Data.Config;

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

		// Many-to-Many
		builder.HasMany(x => x.Participants)
				.WithMany(x => x.Sections)
				.UsingEntity<Enrollment>();  // Join table


		builder.OwnsOne(x => x.TimeSlot, ts =>
		{
			ts.Property(p => p.StartTime).HasColumnType("time(0)").HasColumnName("StartTime").IsRequired();
			ts.Property(p => p.EndTime).HasColumnType("time(0)").HasColumnName("EndTime").IsRequired();
		});

		builder.OwnsOne(x => x.DateRange, dr =>
		{
			dr.Property(p => p.StartDate).HasColumnType("date").HasColumnName("StartDate").IsRequired();
			dr.Property(p => p.EndDate).HasColumnType("date").HasColumnName("EndDate").IsRequired();
		});


		// // adds (WHERE StartDate >= date of two years ago) in the end of every query on the sections
		// var twoYearsDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2));
		// builder.HasQueryFilter(x => x.DateRange.StartDate >= twoYearsDate);
		
		// default data
		// builder.HasData(SeedData.LoadSections());
	}
}
