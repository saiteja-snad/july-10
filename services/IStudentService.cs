using SMS.DTOS.StudentDtos;

namespace StudentManagementSystem.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentResponseDTO> CreateStudentAsync(
            CreateStudentRequestDTO dto);

        Task<List<StudentResponseDTO>> GetAllStudentsAsync();

        Task<StudentResponseDTO?> GetStudentByIdAsync(
            int studentId);

        Task<StudentResponseDTO> UpdateStudentAsync(
            int studentId,
            UpdateStudentRequestDTO dto);

        Task ChangeStudentStatusAsync(
            int studentId,
            string status);
    }
}