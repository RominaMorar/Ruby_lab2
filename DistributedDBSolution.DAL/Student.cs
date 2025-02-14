﻿namespace DistributedDBSolution.DAL;

public class Student
 {
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; } 
    public ICollection<StudentCourse> StudentCourses { get; set; }
}
