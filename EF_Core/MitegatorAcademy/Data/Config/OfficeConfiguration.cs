using System.Collections.Generic;
using EF_Core.MitegatorAcademy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.MitegatorAcademy.Data.Config;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable("Offices");
        
        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.OfficeName).HasColumnType("VARCHAR") // varchar(50)
                                            .HasMaxLength(50)
                                            .IsRequired();  // NOT NULL

        builder.Property(x => x.OfficeLocation).HasColumnType("VARCHAR") // varchar(50)
                                                .HasMaxLength(50)
                                                .IsRequired();  // NOT NULL

        // default data
        // builder.HasData(SeedData.LoadOffices());
    }
}