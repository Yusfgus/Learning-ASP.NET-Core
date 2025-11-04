using System.Collections.Generic;
using EF_Core.MitegatorAcademy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.MitegatorAcademy.Data.Config;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");

        builder.HasKey(x => new { x.SectionId, x.ParticipantId });

        // builder.HasData(SeedData.LoadEnrollments());
    }
}