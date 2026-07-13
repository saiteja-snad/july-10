using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;

public class ResultRepository : IResultRepository
{
    private readonly ApplicationDbContext _context;

    public ResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CreateAsync(Result result)
    {
        await _context.Results.AddAsync(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<List<Result>> GetStudentResultsAsync(int studentId)
    {
        return await _context.Results
            .Where(x => x.StudentId == studentId)
            .Include(x => x.Student)
            .Include(x => x.Exam)
                .ThenInclude(x => x.Course)
            .ToListAsync();
    }

    public async Task<List<Result>> GetExamResultsAsync(int examId)
    {
        return await _context.Results
            .Where(x => x.ExamId == examId)
            .Include(x => x.Student)
            .Include(x => x.Exam)
                .ThenInclude(x => x.Course)
            .ToListAsync();
    }
    public async Task<Result?> GetByIdAsync(int resultId)
    {
        return await _context.Results
            .FirstOrDefaultAsync(x => x.ResultId == resultId);
    }

    public async Task UpdateAsync(Result result)
    {
        _context.Results.Update(result);
        await _context.SaveChangesAsync();
    }
}