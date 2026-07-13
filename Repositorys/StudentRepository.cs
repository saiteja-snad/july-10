using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;


public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Student> CreateAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students
            .Include(s => s.Class)
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int studentId)
    {
        return await _context.Students
            .Include(s => s.Class)
            .FirstOrDefaultAsync(x => x.StudentId == studentId);
    }

    public async Task<Student?> GetByCodeAsync(string studentCode)
    {
        return await _context.Students
            .FirstOrDefaultAsync(x => x.StudentCode == studentCode);
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int studentId)
    {
        var student = await GetByIdAsync(studentId);

        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int studentId)
    {
        return await _context.Students
            .AnyAsync(x => x.StudentId == studentId);
    }
}