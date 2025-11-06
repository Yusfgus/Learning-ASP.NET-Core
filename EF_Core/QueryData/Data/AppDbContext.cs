using System;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core.QueryData.Data;

public class AppDbContext: DbContext
{
    public DbSet<Office> Offices { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<SectionDetails> SectionDetails { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

        string connectionString = config.GetSection("constr").Value!;
        // Console.WriteLine(connectionString + '\n');

        optionsBuilder.UseSqlServer(connectionString)
            .LogTo(WriteSqlQuery, Microsoft.Extensions.Logging.LogLevel.Information);  // log sql queries in the console
    }

    public void WriteSqlQuery(string s)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("=================================== Sql Query ====================================");
        Console.WriteLine(s);
        Console.WriteLine("==================================================================================");
        Console.ResetColor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // add configurations
        // // Method 1 (add one by one)
        // modelBuilder.ApplyConfiguration(new CourseConfiguration());
        // modelBuilder.ApplyConfiguration(new InstructorConfiguration());
        // ...

        // Method 2 (search for configurations in the assembly/project)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}