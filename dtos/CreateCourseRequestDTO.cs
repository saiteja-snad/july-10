namespace SMS.DTOS.CourseDtos
{
    public class CreateCourseRequestDTO
    {
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}
