using SMS.Models;

namespace SMS.Repositorys
{
    public interface IExamRepository
    {
        Task<Exam> CreateAsync(Exam exam);

        Task<List<Exam>> GetAllAsync();

        Task<Exam?> GetByIdAsync(int examId);
    }
}
