using SMS.DTOS.Exam_ResultDtos;

namespace StudentManagementSystem.Services.Interfaces
{
    public interface IResultService
    {
        Task PublishResultAsync(PublishResultRequestDTO dto);

        Task<List<ResultResponseDTO>>GetStudentResultsAsync( int studentId);

        Task<List<ResultResponseDTO>>GetExamResultsAsync(int examId);

       

    }
}