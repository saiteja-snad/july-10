using SMS.Models;

namespace SMS.Repositorys
{
    public interface IResultRepository
    {
        Task<Result> CreateAsync(Result result);

        Task<List<Result>>
            GetStudentResultsAsync(
            int studentId);

        Task<List<Result>>
            GetExamResultsAsync(
            int examId);

        Task<Result?> GetByIdAsync(
            int resultId);

        Task UpdateAsync(Result result);
    }
}
