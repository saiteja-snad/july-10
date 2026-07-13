using SMS.Models;

namespace SMS.Repositorys
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);

        Task<List<Student>> GetAllAsync();

        Task<Student?> GetByIdAsync(int studentId);

        Task<Student?> GetByCodeAsync(string studentCode);

        Task UpdateAsync(Student student);

        Task DeleteAsync(int studentId);

        Task<bool> ExistsAsync(int studentId);
    }
}
