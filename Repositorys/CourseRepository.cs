using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Course> CreateAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int courseId)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(x => x.CourseId == courseId);
    }

    public async Task<Course?> GetByCodeAsync(string courseCode)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(x => x.CourseCode == courseCode);
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }
}