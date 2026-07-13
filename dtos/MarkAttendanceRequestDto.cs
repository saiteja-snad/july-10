namespace SMS.DTOS.AttendancedDtos
{
    public class MarkAttendanceRequestDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = "PRESENT";
        public int MarkedBy { get; set; }
    }
}
