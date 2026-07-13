using SMS.Models;

namespace SMS.Repositorys
{
    public interface IAttendanceRepository
    {
        Task<Attendance> CreateAsync(
            Attendance attendance);

        Task<Attendance?> GetByIdAsync(
            int attendanceId);

        Task<List<Attendance>>
            GetStudentAttendanceAsync(
            int studentId);

        Task<List<Attendance>>
            GetClassAttendanceAsync(
            int classId);

        Task UpdateAsync(
            Attendance attendance);
    }

}
