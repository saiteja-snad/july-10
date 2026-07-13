using SMS.DTOS.CourseDtos;
using SMS.Models;
using SMS.Repositorys;
using StudentManagementSystem.Services.Interfaces;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseResponseDTO>
        CreateCourseAsync(CreateCourseRequestDTO dto)
    {
        var course = new Course
        {
            CourseCode = dto.CourseCode,
            CourseName = dto.CourseName,
            Description = dto.Description,
            Credits = dto.Credits
        };

        await _courseRepository.CreateAsync(course);

        return new CourseResponseDTO
{
    CourseId = course.CourseId,
    CourseCode = course.CourseCode,
    CourseName = course.CourseName,
    Description = course.Description,
    Credits = course.Credits??0,
    DepartmentId = course.DepartmentId??0
};
    }

    public async Task<List<CourseResponseDTO>>
        GetAllCoursesAsync()
    {
        var courses =
            await _courseRepository.GetAllAsync();

        return courses.Select(x => new CourseResponseDTO
        {
            CourseId = x.CourseId,
            CourseCode = x.CourseCode,
            CourseName = x.CourseName,
            Description = x.Description,
            Credits = x.Credits ?? 0,
            DepartmentId = x.DepartmentId ?? 0
        }).ToList();
    }

    public async Task<CourseResponseDTO?>
        GetCourseByIdAsync(int courseId)
    {
        var course =
            await _courseRepository.GetByIdAsync(courseId);

        if (course == null)
            return null;

        return new CourseResponseDTO
        {
            CourseId = course.CourseId,
            CourseCode = course.CourseCode,
            CourseName = course.CourseName,
            Description = course.Description,
            Credits = course.Credits??0,
            DepartmentId = course.DepartmentId ?? 0
        };

    }

    public async Task<CourseResponseDTO>
        UpdateCourseAsync(
        int courseId,
        UpdateCourseRequestDTO dto)
    {
        var course =
            await _courseRepository.GetByIdAsync(courseId);

        if (course == null)
            throw new Exception("Course Not Found");

        course.CourseName = dto.CourseName;
        course.Description = dto.Description;
        course.Credits = dto.Credits;

        await _courseRepository.UpdateAsync(course);

        return await GetCourseByIdAsync(courseId);
    }
}