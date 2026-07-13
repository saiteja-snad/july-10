using System;
using System.Collections.Generic;

namespace SMS.Models;

public partial class Fee
{
    public int FeeId { get; set; }

    public int StudentId { get; set; }

    public string? FeeType { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? DueDate { get; set; }

    public decimal? PaidAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Student Student { get; set; } = null!;
}
