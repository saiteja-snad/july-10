namespace SMS.DTOS.Exam_ResultDtos
{

   
        public class CreateExamResponseDTO
        {
            public string ExamName { get; set; } = string.Empty;
            public int CourseId { get; set; }
            public string CourseName { get; set; } = string.Empty;
            public DateOnly? ExamDate { get; set; }
            public int TotalMarks { get; set; }
        }
    }

