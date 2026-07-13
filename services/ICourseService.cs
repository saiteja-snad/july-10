using SMS.DTOS.CourseDtos;

namespace StudentManagementSystem.Services.Interfaces
{
    public interface ICourseService
    {
        Task<CourseResponseDTO> CreateCourseAsync(
            CreateCourseRequestDTO dto);

        Task<List<CourseResponseDTO>> GetAllCoursesAsync();

        Task<CourseResponseDTO?> GetCourseByIdAsync(
            int courseId);

        Task<CourseResponseDTO> UpdateCourseAsync(
            int courseId,
            UpdateCourseRequestDTO dto);
    }
}