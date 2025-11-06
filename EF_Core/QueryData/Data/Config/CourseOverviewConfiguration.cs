using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.QueryData.Data.Config;

public class CourseOverviewConfiguration : IEntityTypeConfiguration<CourseOverview>
{
    public void Configure(EntityTypeBuilder<CourseOverview> builder)
    {
        builder.HasNoKey();

        builder.ToView("CourseOverview");
    }
}
