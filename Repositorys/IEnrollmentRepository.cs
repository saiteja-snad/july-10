using SMS.Models;

namespace SMS.Repositorys
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> CreateAsync(
            Enrollment enrollment);

        Task<List<Enrollment>>
            GetStudentEnrollmentsAsync(
            int studentId);

        Task<List<Enrollment>>
            GetCourseEnrollmentsAsync(
            int courseId);

        Task<Enrollment?> GetByIdAsync(
            int enrollmentId);

        Task UpdateAsync(
            Enrollment enrollment);
    }
}
