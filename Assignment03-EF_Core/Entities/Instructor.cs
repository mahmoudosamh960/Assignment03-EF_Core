namespace Assignment03_EF_Core.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Bio { get; set; }
    public DateTime JoinedDate { get; set; }

    public ICollection<Course> Courses { get; set; }

}