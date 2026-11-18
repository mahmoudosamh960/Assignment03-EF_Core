using Microsoft.EntityFrameworkCore;
using Assignment03_EF_Core.Data;
using Assignment03_EF_Core.Entities;
using Assignment03_EF_Core.Entities.Enums;

namespace Assignment03_EF_Core.Operations;

public class PlatformOperations
{
    private readonly AppDbContext _context;

    public PlatformOperations(AppDbContext context)
    {
        _context = context;
    }


    public void AddInstructor()
    {
        var instructor = new Instructor
        {
            FullName = "New Instructor",
            Email = "new.instructor@example.com",
            Bio = "C# Instructor",
            JoinedDate = DateTime.Today
        };

        _context.Instructors.Add(instructor);
        _context.SaveChanges();

        Console.WriteLine($"Instructor added. ID: {instructor.Id}");
    }

    
    public void AddCourse()
    {
        int instructorId = 1;
        int categoryId = 2;

        bool instructorExists =
            _context.Instructors.Any(i => i.Id == instructorId);

        bool categoryExists =
            _context.Categories.Any(c => c.Id == categoryId);

        if (!instructorExists || !categoryExists)
        {
            Console.WriteLine("Instructor or Category not found.");
            return;
        }

        var course = new Course
        {
            Title = "New EF Core Course",
            Description = "Learning Entity Framework Core",
            Price = 1800,
            Level = CourseLevel.Intermediate,
            IsPublished = false,
            PublishedDate = null,
            InstructorId = instructorId,
            CategoryId = categoryId
        };

        _context.Courses.Add(course);
        _context.SaveChanges();

        Console.WriteLine($"Course added. ID: {course.Id}");
    }

    
    public void AddSeveralStudents()
    {
        var students = new List<Student>
        {
            new Student
            {
                FullName = "Student One",
                Email = "student1@example.com",
                DateOfBirth = new DateTime(2005, 1, 1)
            },
            new Student
            {
                FullName = "Student Two",
                Email = "student2@example.com",
                DateOfBirth = new DateTime(2005, 2, 2)
            },
            new Student
            {
                FullName = "Student Three",
                Email = "student3@example.com",
                DateOfBirth = new DateTime(2005, 3, 3)
            }
        };

        _context.Students.AddRange(students);
        _context.SaveChanges();

        foreach (var student in students)
        {
            Console.WriteLine($"Student ID: {student.Id}");
        }
    }

   
    public void EnrollStudent()
    {
        int studentId = 3;
        int courseId = 1;

        bool studentExists =
            _context.Students.Any(s => s.Id == studentId);

        var course = _context.Courses.Find(courseId);

        if (!studentExists)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        if (!course.IsPublished)
        {
            Console.WriteLine("Enrollment refused: Course is not published.");
            return;
        }

        bool alreadyEnrolled = _context.Enrollments.Any(e =>
            e.StudentId == studentId &&
            e.CourseId == courseId);

        if (alreadyEnrolled)
        {
            Console.WriteLine("Student is already enrolled.");
            return;
        }

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            EnrollmentDate = DateTime.Today,
            ProgressPercentage = 0,
            Status = EnrollmentStatus.Active,
            FinalGrade = null
        };

        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();

        Console.WriteLine("Student enrolled successfully.");
    }

    
    public void FindCourse()
    {
        int courseId = 1;

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            course = _context.Courses.FirstOrDefault(
                c => c.Title == "C# Basics");
        }

        if (course == null)
        {
            Console.WriteLine("Course not found.");
        }
        else
        {
            Console.WriteLine($"Course: {course.Title}");
        }
    }

    
    public void FilterCourses()
    {
        decimal minPrice = 900;
        decimal maxPrice = 1600;

        var courses = _context.Courses
            .Where(c => c.Price >= minPrice &&
                        c.Price <= maxPrice)
            .OrderByDescending(c => c.Price)
            .Select(c => new
            {
                c.Title,
                c.Level,
                c.Price
            })
            .ToList();

        foreach (var course in courses)
        {
            Console.WriteLine(
                $"{course.Title} | {course.Level} | {course.Price}");
        }
    }

    
    public void PrintSubCategories()
    {
        int categoryId = 1;

        var subCategories = _context.Categories
            .Where(c => c.ParentCategoryId == categoryId)
            .ToList();

        Console.WriteLine("Sub-Categories:");

        foreach (var category in subCategories)
        {
            Console.WriteLine(category.Name);
        }

        var topCategories = _context.Categories
            .Where(c => c.ParentCategoryId == null)
            .ToList();

        Console.WriteLine("Top-Level Categories:");

        foreach (var category in topCategories)
        {
            Console.WriteLine(category.Name);
        }
    }

    
    public void PrintCourseRoster()
    {
        int courseId = 1;

        var studentIds = _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.StudentId)
            .ToList();

        var students = _context.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToList();

        Console.WriteLine(
            $"Number of Students: {studentIds.Count}");

        foreach (var student in students)
        {
            Console.WriteLine(student.FullName);
        }
    }

    
    public void ChangeCoursePrice()
    {
        int courseId = 1;

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        course.Price = 1350;

        _context.SaveChanges();

        Console.WriteLine("Course price updated.");
    }

    
    public void UpdateEnrollmentProgress()
    {
        int studentId = 2;
        int courseId = 1;

        decimal newProgress = 100;

        if (newProgress < 0 || newProgress > 100)
        {
            Console.WriteLine("Progress must be between 0 and 100.");
            return;
        }

        var enrollment =
            _context.Enrollments.Find(studentId, courseId);

        if (enrollment == null)
        {
            Console.WriteLine("Enrollment not found.");
            return;
        }

        enrollment.ProgressPercentage = newProgress;

        if (newProgress == 100)
        {
            enrollment.Status = EnrollmentStatus.Completed;
        }

        _context.SaveChanges();

        Console.WriteLine("Progress updated successfully.");
    }

    
    public void PublishCourse()
    {
        int courseId = 3;

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        course.IsPublished = true;
        course.PublishedDate = DateTime.Today;

        _context.SaveChanges();

        Console.WriteLine("Course published successfully.");
    }

    
    public void UnenrollStudent()
    {
        int studentId = 2;
        int courseId = 1;

        var enrollment =
            _context.Enrollments.Find(studentId, courseId);

        if (enrollment == null)
        {
            Console.WriteLine("Enrollment not found.");
            return;
        }

        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();

        Console.WriteLine("Student unenrolled successfully.");
    }

    
    public void DeleteCategory()
    {
        int categoryId = 1;

        var category = _context.Categories.Find(categoryId);

        if (category == null)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        
        _context.Categories.Remove(category);

        try
        {
            _context.SaveChanges();
            Console.WriteLine("Category deleted.");
            return;
        }
        catch (DbUpdateException)
        {
            _context.Entry(category).State = EntityState.Unchanged;

            Console.WriteLine(
                "Delete failed: Category still has sub-categories.");
        }

       
        bool hasSubCategories = _context.Categories
            .Any(c => c.ParentCategoryId == categoryId);

        if (hasSubCategories)
        {
            Console.WriteLine(
                "Cannot delete this category because it has sub-categories.");
            return;
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        Console.WriteLine("Category deleted safely.");
    }

    
    public void DeleteCourse()
    {
        int courseId = 2;

        var course = _context.Courses.Find(courseId);

        if (course == null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        int beforeCount = _context.Enrollments
            .Count(e => e.CourseId == courseId);

        Console.WriteLine(
            $"Enrollments before delete: {beforeCount}");

        _context.Courses.Remove(course);
        _context.SaveChanges();

        int afterCount = _context.Enrollments
            .Count(e => e.CourseId == courseId);

        Console.WriteLine(
            $"Enrollments after delete: {afterCount}");

        
    }
}