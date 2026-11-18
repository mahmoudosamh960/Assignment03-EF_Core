using Assignment03_EF_Core.Entities.Enums;
namespace Assignment03_EF_Core.Entities;

public class Enrollment
{
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public DateTime EnrollmentDate { get; set; }
    public decimal ProgressPercentage { get; set; }
    public EnrollmentStatus Status { get; set; }
    public decimal? FinalGrade { get; set; }
}