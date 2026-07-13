using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int roleId)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(x => x.RoleId == roleId);
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(x => x.RoleName == roleName);
    }
}