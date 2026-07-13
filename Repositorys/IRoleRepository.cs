using SMS.Models;

namespace SMS.Repositorys
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int roleId);

        Task<Role?> GetByNameAsync(string roleName);
    }
}
