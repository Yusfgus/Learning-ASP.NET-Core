

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
### Result: Table for all types
---


## 3- Table Per Concrete ( TPC )
```code
public abstract class Quiz {}

public class MultipleChoiceQuiz : Quiz {}

public class TrueAndFalseQuiz : Quiz {}

```
```code
public class AppDbContext : DbContext
{
    // ...
    public DbSet<MultipleChoiceQuiz> MultipleChoiceQuizzes { get; set; }
    public DbSet<TrueAndFalseQuiz> TrueAndFalseQuizzes { get; set; }
    // ...
}
```
```code
public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        // ...
        builder.UseTpcMappingStrategy();
        // ...
    }
}
```
### Result: One table for all non-abstract types
---

