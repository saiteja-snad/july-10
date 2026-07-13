using SMS.DTOS.UserDtos;


namespace StudentManagementSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDTO> LoginAsync(
            LoginRequestDTO request);
    }
}