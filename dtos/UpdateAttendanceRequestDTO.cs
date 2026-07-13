namespace SMS.DTOS.AttendancedDtos
{
    public class UpdateAttendanceRequestDTO
    {
        public string Status { get; set; } = "PRESENT";
        public int MarkedBy { get; set; }
    }
}
