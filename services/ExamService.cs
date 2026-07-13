using SMS.DTOS.Exam_ResultDtos;
using SMS.Models;
using SMS.Repositorys;
using StudentManagementSystem.Services.Interfaces;

public class ExamService : IExamService
{
    private readonly IExamRepository _repository;

    public ExamService(IExamRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateExamAsync( CreateExamRequestDTO dto)
    {
        var exam = new Exam
        {
            ExamName = dto.ExamName,
            CourseId = dto.CourseId,
            ExamDate =dto.ExamDate,
            TotalMarks = dto.TotalMarks
        };

        await _repository.CreateAsync(exam);
    }

    public async Task<List<CreateExamResponseDTO>> GetAllExamsAsync()
    {

        var exams = await _repository.GetAllAsync();


        return exams.Select(x => new CreateExamResponseDTO
        {
            ExamName = x.ExamName,
            CourseId = x.CourseId??0,
            CourseName = x.Course?.CourseName ?? string.Empty,
            ExamDate = x.ExamDate,
            TotalMarks = x.TotalMarks??0
        }).ToList();


    }
}