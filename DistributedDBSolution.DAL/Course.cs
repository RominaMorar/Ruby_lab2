namespace DistributedDBSolution.DAL;

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public string Description { get; set; }
    public int TeacherId { get; set; } 
    public Teacher Teacher { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
}