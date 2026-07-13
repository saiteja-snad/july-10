using SMS.Models;

namespace SMS.Repositorys
{
    public interface IClassRepository
    {
        Task<List<Class>> GetAllAsync();

        Task<Class?> GetByIdAsync(int classId);

        Task<Class> CreateAsync(Class classEntity);

        Task UpdateAsync(Class classEntity);
    }
}
