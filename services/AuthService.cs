using SMS.DTOS.UserDtos;
using SMS.Repositorys;

using StudentManagementSystem.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<UserResponseDTO> LoginAsync(
        LoginRequestDTO request)
    {
        var user = await _userRepository
            .GetByUsernameAsync(request.Username);

        if (user == null)
            throw new Exception("Invalid Username");

        if (user.PasswordHash != request.Password)
            throw new Exception("Invalid Password");

        return new UserResponseDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            RoleName = user.Role.RoleName,
            Token = _jwtService.GenerateToken(user)
        };
    }
}