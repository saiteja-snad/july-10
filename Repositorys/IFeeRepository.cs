using SMS.Models;

namespace SMS.Repositorys
{
    
        public interface IFeeRepository
        {
            Task<Fee> CreateAsync(Fee fee);

            Task<Fee?> GetByIdAsync(int feeId);

            Task<List<Fee>>
                GetStudentFeesAsync(
                int studentId);

            Task UpdateAsync(Fee fee);
       
    }
}
