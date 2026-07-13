using SMS.DTOS.EnrollmentDtos;
using SMS.Models;
using SMS.Repositorys;
using StudentManagementSystem.Services.Interfaces;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repository;

    public EnrollmentService(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateEnrollmentAsync(CreateEnrollmentRequestDTO dto)
    {
        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
            AcademicYear = dto.AcademicYear,
            Semester = dto.Semester
        };
        await _repository.CreateAsync(enrollment);
    }

    public async Task<List<EnrollmentResponseDTO>> GetStudentEnrollmentsAsync(int studentId)
    {
        var enrollments = await _repository.GetStudentEnrollmentsAsync(studentId);

        return enrollments.Select(x => new EnrollmentResponseDTO
        {
            EnrollmentId = x.EnrollmentId,
            StudentId = x.StudentId,
            CourseId = x.CourseId,
            AcademicYear = x.AcademicYear,
            Semester = x.Semester,
            Status = x.Status
        }).ToList();
    }

    public async Task<List<EnrollmentResponseDTO>> GetCourseEnrollmentsAsync(int courseId)
    {
        var enrollments = await _repository.GetCourseEnrollmentsAsync(courseId);

        return enrollments.Select(x => new EnrollmentResponseDTO
        {
            EnrollmentId = x.EnrollmentId,
            StudentId = x.StudentId,
            CourseId = x.CourseId,
            AcademicYear = x.AcademicYear,
            Semester = x.Semester,
            EnrollmentDate = x.EnrollmentDate,
            Status = x.Status
        }).ToList();
    }



    public async Task<EnrollmentResponseDTO?> GetEnrollmentByIdAsync(int enrollmentId)
    {

        var enrollment = await _repository.GetByIdAsync(enrollmentId);
        if (enrollment == null)
            return null;

        return new EnrollmentResponseDTO
        {
            EnrollmentId = enrollment.EnrollmentId,
            StudentId = enrollment.StudentId,
            CourseId = enrollment.CourseId,
            StudentName = enrollment.Student?.FirstName,
            CourseName = enrollment.Course?.CourseName ?? string.Empty,
            AcademicYear = enrollment.AcademicYear,
            Semester = enrollment.Semester,
            EnrollmentDate = enrollment.EnrollmentDate,
            Status = enrollment.Status
        };


    }

    public async Task<bool> UpdateEnrollmentAsync(int enrollmentId, CreateEnrollmentRequestDTO dto)
    {

        var enrollment = await _repository.GetByIdAsync(enrollmentId);

        if (enrollment == null)
            return false;

        enrollment.StudentId = dto.StudentId;
        enrollment.CourseId = dto.CourseId;
        enrollment.AcademicYear = dto.AcademicYear;
        enrollment.Semester = dto.Semester;

        await _repository.UpdateAsync(enrollment);

        return true;
    }
}

