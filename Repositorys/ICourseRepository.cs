using SMS.Models;

namespace SMS.Repositorys
{
    public interface ICourseRepository
    {
        Task<Course> CreateAsync(Course course);

        Task<List<Course>> GetAllAsync();

        Task<Course?> GetByIdAsync(int courseId);

        Task<Course?> GetByCodeAsync(string courseCode);

        Task UpdateAsync(Course course);
    }
}
