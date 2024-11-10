using MongoDB.Driver;
using Microsoft.Extensions.Options;
using DistributedDBSolution.DAL;
using DistributedDBSolution.DAL.Settings;

namespace DistributedDBSolution.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<Student>("Students");
        }

        public async Task<List<Student>> GetAsync() => await _studentsCollection.Find(s => true).ToListAsync();
        
        // Changed GetByIdAsync to use StudentId
        public async Task<Student> GetByIdAsync(int id) => await _studentsCollection.Find(s => s.StudentId == id).FirstOrDefaultAsync();
        
        public async Task CreateAsync(Student student) => await _studentsCollection.InsertOneAsync(student);

        // Changed UpdateAsync to use StudentId
        public async Task UpdateAsync(int studentId, Student updatedStudent) => await _studentsCollection.ReplaceOneAsync(s => s.StudentId == studentId, updatedStudent);

        // Changed RemoveAsync to use StudentId
        public async Task RemoveAsync(int studentId) => await _studentsCollection.DeleteOneAsync(s => s.StudentId == studentId);
    }
}
