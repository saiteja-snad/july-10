using SMS.DTOS.FeeDtos;

namespace StudentManagementSystem.Services.Interfaces
{
    public interface IFeeService
    {
        Task CreateFeeAsync(
            CreateFeeRequestDTO dto);

        Task PayFeeAsync(
            int feeId,
            PayFeeRequestDTO dto);

        Task<List<FeeResponseDTO>>
            GetStudentFeesAsync(
            int studentId);
    }
}