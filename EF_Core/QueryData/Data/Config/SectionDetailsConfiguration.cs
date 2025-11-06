using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.QueryData.Data.Config;

public class SectionDetailsConfiguration : IEntityTypeConfiguration<SectionDetails>
{
    public void Configure(EntityTypeBuilder<SectionDetails> builder)
    {
        builder.HasNoKey();
    }
}
