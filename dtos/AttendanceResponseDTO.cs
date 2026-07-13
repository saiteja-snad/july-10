namespace SMS.DTOS.AttendancedDtos
{
    public class AttendanceResponseDTO
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string MarkedByUsername { get; set; } = string.Empty;
    }
}
