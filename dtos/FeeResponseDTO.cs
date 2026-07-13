public class FeeResponseDTO
{
    public int FeeId { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string FeeType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal BalanceAmount => Amount - PaidAmount;

    public DateTime DueDate { get; set; }

    public string Status { get; set; } = string.Empty;
}