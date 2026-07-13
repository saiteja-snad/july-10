using SMS.DTOS.FeeDtos;
using SMS.Models;
using SMS.Repositorys;
using StudentManagementSystem.Services.Interfaces;

public class FeeService : IFeeService
{
    private readonly IFeeRepository _repository;

    public FeeService(IFeeRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateFeeAsync(
        CreateFeeRequestDTO dto)
    {
        var fee = new Fee
        {
            StudentId = dto.StudentId,
            FeeType = dto.FeeType,
            Amount = dto.Amount,
            DueDate = DateOnly.FromDateTime(dto.DueDate)
        };

        await _repository.CreateAsync(fee);
    }

    public async Task PayFeeAsync(
        int feeId,
        PayFeeRequestDTO dto)
    {
        var fee = await _repository.GetByIdAsync(feeId);

        fee.PaidAmount += dto.PaymentAmount;

        fee.Status =
            fee.PaidAmount >= fee.Amount
            ? "PAID"
            : "PARTIAL";

        await _repository.UpdateAsync(fee);
    }

    public async Task<List<FeeResponseDTO>>
    GetStudentFeesAsync(int studentId)
    {
        var fees =
            await _repository.GetStudentFeesAsync(studentId);

        return fees.Select(x => new FeeResponseDTO
        {
            FeeId = x.FeeId,
            StudentId = x.StudentId,
            FeeType = x.FeeType,
            Amount = x.Amount ??0,
            PaidAmount = x.PaidAmount??0,
            Status = x.Status
        }).ToList();
    }
}