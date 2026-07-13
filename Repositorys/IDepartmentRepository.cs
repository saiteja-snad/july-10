using SMS.Models;

namespace SMS.Repositorys
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();

        Task<Department?> GetByIdAsync(int departmentId);

        Task<Department> CreateAsync(Department department);

        Task UpdateAsync(Department department);
    }
}
