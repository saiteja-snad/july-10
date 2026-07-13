using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment> CreateAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<List<Enrollment>> GetStudentEnrollmentsAsync(int studentId)
    {
        return await _context.Enrollments
            .Where(x => x.StudentId == studentId)
            .Include(x => x.Course)
            .ToListAsync();
    }

    public async Task<List<Enrollment>> GetCourseEnrollmentsAsync(int courseId)
    {
        return await _context.Enrollments
            .Where(x => x.CourseId == courseId)
            .Include(x => x.Student)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetByIdAsync(int enrollmentId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(x => x.EnrollmentId == enrollmentId);
    }

    public async Task UpdateAsync(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync();
    }
}