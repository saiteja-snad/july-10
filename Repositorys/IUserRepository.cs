using SMS.Models;

namespace SMS.Repositorys
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId);

        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
    }

}
