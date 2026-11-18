using Microsoft.EntityFrameworkCore;
using Assignment03_EF_Core.Entities;
using Assignment03_EF_Core.Entities.Enums;

namespace Assignment03_EF_Core.Data;

public static class SeedData
{
    public static void AddSeedData(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<Instructor>().HasData(
            new Instructor
            {
                Id = 1,
                FullName = "Ahmed Hassan",
                Email = "ahmed@example.com",
                Bio = "C# Instructor",
                JoinedDate = new DateTime(2024, 1, 10)
            },
            new Instructor
            {
                Id = 2,
                FullName = "Mona Ali",
                Email = "mona@example.com",
                Bio = "Database Instructor",
                JoinedDate = new DateTime(2024, 2, 15)
            }
        );

    
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Programming",
                Description = "Programming Courses",
                ParentCategoryId = null
            },
            new Category
            {
                Id = 2,
                Name = "Backend",
                Description = "Backend Development",
                ParentCategoryId = 1
            },
            new Category
            {
                Id = 3,
                Name = "Databases",
                Description = "Database Courses",
                ParentCategoryId = 1
            }
        );

        
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "C# Basics",
                Description = "Introduction to C#",
                Price = 1000,
                Level = CourseLevel.Beginner,
                IsPublished = true,
                PublishedDate = new DateTime(2025, 1, 1),
                InstructorId = 1,
                CategoryId = 2
            },
            new Course
            {
                Id = 2,
                Title = "SQL Server Basics",
                Description = "Introduction to SQL Server",
                Price = 1200,
                Level = CourseLevel.Beginner,
                IsPublished = true,
                PublishedDate = new DateTime(2025, 2, 1),
                InstructorId = 2,
                CategoryId = 3
            },
            new Course
            {
                Id = 3,
                Title = "EF Core",
                Description = "Entity Framework Core",
                Price = 1500,
                Level = CourseLevel.Intermediate,
                IsPublished = false,
                PublishedDate = null,
                InstructorId = 1,
                CategoryId = 2
            }
        );

        
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                FullName = "Omar Samy",
                Email = "omar@example.com",
                DateOfBirth = new DateTime(2005, 5, 10)
            },
            new Student
            {
                Id = 2,
                FullName = "Sara Mohamed",
                Email = "sara@example.com",
                DateOfBirth = new DateTime(2004, 8, 20)
            },
            new Student
            {
                Id = 3,
                FullName = "Youssef Adel",
                Email = "youssef@example.com",
                DateOfBirth = new DateTime(2005, 3, 12)
            }
        );

       
        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment
            {
                StudentId = 1,
                CourseId = 1,
                EnrollmentDate = new DateTime(2025, 3, 1),
                ProgressPercentage = 100,
                Status = EnrollmentStatus.Completed,
                FinalGrade = 95
            },
            new Enrollment
            {
                StudentId = 2,
                CourseId = 1,
                EnrollmentDate = new DateTime(2025, 3, 2),
                ProgressPercentage = 40,
                Status = EnrollmentStatus.Active,
                FinalGrade = null
            },
            new Enrollment
            {
                StudentId = 3,
                CourseId = 2,
                EnrollmentDate = new DateTime(2025, 3, 3),
                ProgressPercentage = 20,
                Status = EnrollmentStatus.Active,
                FinalGrade = null
            }
        );
    }

}