using System;
using System.Collections.Generic;

namespace SMS.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public string? ExamName { get; set; }

    public int? CourseId { get; set; }

    public DateOnly? ExamDate { get; set; }

    public int? TotalMarks { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
