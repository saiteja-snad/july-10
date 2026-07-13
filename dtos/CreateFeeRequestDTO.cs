namespace SMS.DTOS.FeeDtos
{
    public class CreateFeeRequestDTO
    {
        public int StudentId { get; set; }
        public string FeeType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}
