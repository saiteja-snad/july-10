using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;

public class ExamRepository : IExamRepository
{
    private readonly ApplicationDbContext _context;

    public ExamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Exam> CreateAsync(Exam exam)
    {
        await _context.Exams.AddAsync(exam);
        await _context.SaveChangesAsync();
        return exam;
    }

    public async Task<List<Exam>> GetAllAsync()
    {
        return await _context.Exams.ToListAsync();
    }

    public async Task<Exam?> GetByIdAsync(int examId)
    {
        return await _context.Exams
            .FirstOrDefaultAsync(x => x.ExamId == examId);
    }
}