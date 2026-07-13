using SMS.DTOS.Exam_ResultDtos;

namespace StudentManagementSystem.Services.Interfaces
{
    public interface IExamService
    {
        Task CreateExamAsync(CreateExamRequestDTO dto);

        Task<List<CreateExamResponseDTO>> GetAllExamsAsync();
    }
}