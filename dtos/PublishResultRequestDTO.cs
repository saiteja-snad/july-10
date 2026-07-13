namespace SMS.DTOS.Exam_ResultDtos
{
    public class PublishResultRequestDTO
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public decimal MarksObtained { get; set; }
        public string Grade { get; set; } = string.Empty;
        public string? Remarks { get; set; }
    }
}
