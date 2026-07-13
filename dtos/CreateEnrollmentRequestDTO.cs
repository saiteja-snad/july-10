namespace SMS.DTOS.EnrollmentDtos
{
    public class CreateEnrollmentRequestDTO
    {
            public int StudentId { get; set; }
            public int CourseId { get; set; }
            public string AcademicYear { get; set; } = string.Empty;
            public string Semester { get; set; } = string.Empty;
        
    }
}
