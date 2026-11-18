using Assignment03_EF_Core.Data;
using Assignment03_EF_Core.Operations;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("ONLINE LEARNING PLATFORM");

using (var context = new AppDbContext())
{
    try
    {
        context.Database.Migrate();

        var operations = new PlatformOperations(context);

        Console.WriteLine("\n===== CREATE =====");

        operations.AddInstructor();

        operations.AddCourse();

        operations.AddSeveralStudents();

        operations.EnrollStudent();


        Console.WriteLine("\n===== READ =====");

        operations.FindCourse();

        operations.FilterCourses();

        operations.PrintSubCategories();

        operations.PrintCourseRoster();


        Console.WriteLine("\n===== UPDATE =====");

        operations.ChangeCoursePrice();

        operations.UpdateEnrollmentProgress();

        operations.PublishCourse();


        Console.WriteLine("\n===== DELETE =====");

        operations.UnenrollStudent();

        operations.DeleteCategory();

        operations.DeleteCourse();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred:");
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine("\nApplication Finished!");