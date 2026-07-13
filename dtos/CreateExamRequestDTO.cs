namespace SMS.DTOS.Exam_ResultDtos
{
    public class CreateExamRequestDTO
    {
        public string ExamName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateOnly? ExamDate { get; set; }
        public int ?TotalMarks { get; set; }
    }
}
