public class ResultResponseDTO
{
    public int ResultId { get; set; }

    public int StudentId { get; set; }

    public int ExamId { get; set; }

    public string ExamName { get; set; } = string.Empty;

    public string StudentName { get; set; } = string.Empty;

    public string CourseName { get; set; } = string.Empty;

    public int TotalMarks { get; set; }

    public decimal MarksObtained { get; set; }

    public string Grade { get; set; } = string.Empty;

    public string? Remarks { get; set; }

    public DateTime PublishedAt { get; set; }
}
