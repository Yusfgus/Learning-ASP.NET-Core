using System;
using EF_Core.MitegatorAcademy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core.MitegatorAcademy.Data;

public class AppDbContext: DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Instructor> Offices { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Corporate> Corporates { get; set; }    
    public DbSet<Section> Sections { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<MultipleChoiceQuiz> MultipleChoiceQuizzes { get; set; }
    public DbSet<TrueAndFalseQuiz> TrueAndFalseQuizzes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

        string connectionString = config.GetSection("constr").Value!;
        // Console.WriteLine(connectionString + '\n');

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // add configurations
        // // Method 1 (add one by one)
        // modelBuilder.ApplyConfiguration(new CourseConfiguration());
        // modelBuilder.ApplyConfiguration(new InstructorConfiguration());

        // Method 2 (search for configurations in the assembly/project)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}