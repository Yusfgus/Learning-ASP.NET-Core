

# Hierarchy

```code
public class Participant {}

public class Individual : Participant{}

public class Corporate : Participant{}

```

## 1- Default Mapping

```code
public class AppDbContext : DbContext
{
    // ...
    public DbSet<Participant> Participants { get; set; }
    // ...
}
```
### Result: One table for parent class only
---


## 2- Table Per Hierarchy ( TPH )

```code
public class AppDbContext : DbContext
{
    // ...
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Corporate> Cooperates { get; set; }    
    // ...
}
```
```code
public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        // ...
        builder.HasDiscriminator<string>("ParticipantType")
            .HasValue<Individual>("INDV")
            .HasValue<Corporate>("COPR");

        builder.Property("ParticipantType")  // instead of Discriminator
                .HasColumnType("VARCHAR")
                .HasMaxLength(4);
        // ...
    }
}
```
### Result: One table for all types with Discriminator property
---


## 3- Table Per Type ( TPT )
### Method 1

```code
public class AppDbContext : DbContext
{
    // ...
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Corporate> Cooperates { get; set; }    
    // ...
}
```
```code
public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        // ...
        builder.UseTptMappingStrategy();
        // ...
    }
}
```
### Method 2

```code
public class AppDbContext : DbContext
{
    // ...
    public DbSet<Participant> Participants { get; set; }   
    // ...
}
```
```code
public class IndividualConfiguration : IEntityTypeConfiguration<Individual>
{
    public void Configure(EntityTypeBuilder<Individual> builder)
    {
        builder.ToTable("Individuals");
    }
}
```
```code
public class CorporateConfiguration : IEntityTypeConfiguration<Corporate>
{
    public void Configure(EntityTypeBuilder<Corporate> builder)
    {
        builder.ToTable("Corprates");
    }
}
```
### Result: Table for each type
---


## 3- Table Per Concrete ( TPC )

```code
public class AppDbContext : DbContext
{
    // ...
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Corporate> Cooperates { get; set; }    
    // ...
}
```
```code
public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        // ...
        builder.HasDiscriminator<string>("ParticipantType")
            .HasValue<Individual>("INDV")
            .HasValue<Corporate>("COPR");

        builder.Property("ParticipantType")  // instead of Discriminator
                .HasColumnType("VARCHAR")
                .HasMaxLength(4);
        // ...
    }
}
```
### Result: One table for all types with Discriminator property
---

