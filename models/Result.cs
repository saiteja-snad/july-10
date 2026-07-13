using System;
using System.Collections.Generic;

namespace SMS.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int ExamId { get; set; }

    public int StudentId { get; set; }

    public decimal? MarksObtained { get; set; }

    public string? Grade { get; set; }

    public string? Remarks { get; set; }

    public DateTime? PublishedAt { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
