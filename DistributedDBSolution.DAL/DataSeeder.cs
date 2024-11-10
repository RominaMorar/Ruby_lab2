using DistributedDBSolution.DAL;
using Microsoft.EntityFrameworkCore;

namespace DistributedDBSolution.Services
{
    public class DataSeedService
    {
        private readonly LabDbContext _context;

        public DataSeedService(LabDbContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            if (!await _context.Students.AnyAsync())
            {
                var students = new List<Student>
                {
                    new Student { FirstName = "John", LastName = "Doe" },
                    new Student { FirstName = "Jane", LastName = "Smith" }
                };

                var courses = new List<Course>
                {
                    new Course { Title = "Math 101", Credits = 3 },
                    new Course { Title = "English 101", Credits = 2 }
                };

                await _context.Students.AddRangeAsync(students);
                await _context.Courses.AddRangeAsync(courses);
                await _context.SaveChangesAsync();
            }
        }
    }
}
