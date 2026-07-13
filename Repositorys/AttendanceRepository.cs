using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Attendance> CreateAsync(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance);
        await _context.SaveChangesAsync();
        return attendance;
    }

    public async Task<Attendance?> GetByIdAsync(int attendanceId)
    {
        return await _context.Attendances
            .FirstOrDefaultAsync(x => x.AttendanceId == attendanceId);
    }

    public async Task<List<Attendance>> GetStudentAttendanceAsync(int studentId)
    {
        return await _context.Attendances
            .Where(x => x.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<List<Attendance>>
    GetClassAttendanceAsync(int classId)
    {
        return await _context.Attendances
            .Include(x => x.Student)
            .Where(x => x.Student != null &&
                        x.Student.ClassId == classId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Attendance attendance)
    {
        _context.Attendances.Update(attendance);
        await _context.SaveChangesAsync();
    }
}