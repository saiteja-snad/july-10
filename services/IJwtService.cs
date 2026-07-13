using SMS.Models;


namespace StudentManagementSystem.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
