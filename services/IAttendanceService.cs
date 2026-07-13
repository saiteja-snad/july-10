using SMS.DTOS.AttendancedDtos;


namespace StudentManagementSystem.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task MarkAttendanceAsync(
            MarkAttendanceRequestDto dto);

        Task UpdateAttendanceAsync(
            int attendanceId,
            UpdateAttendanceRequestDTO dto);

        Task<List<AttendanceResponseDTO>>
            GetStudentAttendanceAsync(
            int studentId);

        Task<List<AttendanceResponseDTO>>
            GetClassAttendanceAsync(
            int classId);
    }
}