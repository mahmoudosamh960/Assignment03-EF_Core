using Microsoft.EntityFrameworkCore;
using Assignment03_EF_Core.Entities;
using Assignment03_EF_Core.Data.Configurations;

namespace Assignment03_EF_Core.Data;

public class AppDbContext : DbContext
{
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=DESKTOP-37EVQ3K\MSSQLSERVER01;Database=OnlineLearningPlatformDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InstructorConfiguration());

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());

        modelBuilder.ApplyConfiguration(new CourseConfiguration());

        modelBuilder.ApplyConfiguration(new StudentConfiguration());

        modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());

        SeedData.AddSeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}