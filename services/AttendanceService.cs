using SMS.DTOS.AttendancedDtos;
using SMS.Models;
using SMS.Repositorys;
using StudentManagementSystem.Services.Interfaces;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _repository;

    public AttendanceService(
        IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public async Task MarkAttendanceAsync(
        MarkAttendanceRequestDto dto)
    {
        var attendance = new Attendance
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
            AttendanceDate = DateOnly.FromDateTime(dto.AttendanceDate),
          
            Status = dto.Status
        };

        await _repository.CreateAsync(attendance);
    }

    public async Task UpdateAttendanceAsync(
        int attendanceId,
        UpdateAttendanceRequestDTO dto)
    {
        var attendance =
            await _repository.GetByIdAsync(attendanceId);

        attendance.Status = dto.Status;

        await _repository.UpdateAsync(attendance);
    }

    public async Task<List<AttendanceResponseDTO>>
    GetStudentAttendanceAsync(int studentId)
    {
        var attendance =
            await _repository.GetStudentAttendanceAsync(studentId);

        return attendance.Select(x => new AttendanceResponseDTO
        {
            AttendanceId = x.AttendanceId,
            StudentId = x.StudentId,
            CourseId = x.CourseId,

          AttendanceDate = x.AttendanceDate.ToDateTime(TimeOnly.MinValue),
            Status = x.Status
        }).ToList();
    }

    public async Task<List<AttendanceResponseDTO>>
        GetClassAttendanceAsync(int classId)
    {
        return (await _repository
            .GetClassAttendanceAsync(classId))
            .Select(x => new AttendanceResponseDTO())
            .ToList();
    }
}