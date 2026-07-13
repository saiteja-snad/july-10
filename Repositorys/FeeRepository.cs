using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Models;
using SMS.Repositorys;

public class FeeRepository : IFeeRepository
{
    private readonly ApplicationDbContext _context;

    public FeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Fee> CreateAsync(Fee fee)
    {
        await _context.Fees.AddAsync(fee);
        await _context.SaveChangesAsync();
        return fee;
    }

    public async Task<Fee?> GetByIdAsync(int feeId)
    {
        return await _context.Fees
            .FirstOrDefaultAsync(x => x.FeeId == feeId);
    }

    public async Task<List<Fee>> GetStudentFeesAsync(int studentId)
    {
        return await _context.Fees
    .Where(x => x.StudentId == studentId)
    .Include(x => x.Student)
    .ToListAsync();
    }

    public async Task UpdateAsync(Fee fee)
    {
        _context.Fees.Update(fee);
        await _context.SaveChangesAsync();
    }
}