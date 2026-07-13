using SMS.DTOS.Exam_ResultDtos;
using SMS.Models;
using SMS.Repositorys;
using StudentManagementSystem.Services.Interfaces;

public class ResultService : IResultService
{
    private readonly IResultRepository _repository;

    public ResultService(IResultRepository repository)
    {
        _repository = repository;
    }

    public async Task PublishResultAsync(PublishResultRequestDTO dto)
    {
        var result = new Result
        {
            ExamId = dto.ExamId,
            StudentId = dto.StudentId,
            MarksObtained = dto.MarksObtained,
            Grade = dto.Grade
        };

        await _repository.CreateAsync(result);
    }

    public async Task<List<ResultResponseDTO>>GetStudentResultsAsync(int studentId)
    {
        var results =
            await _repository.GetStudentResultsAsync(studentId);

        return results.Select(x => new ResultResponseDTO
        {
            ResultId = x.ResultId,
            StudentId = x.StudentId,
            ExamId = x.ExamId,
            MarksObtained = x.MarksObtained??0,
            Grade = x.Grade,
            Remarks = x.Remarks
        }).ToList();
    }

    public async Task<List<ResultResponseDTO>> GetExamResultsAsync(int examId)
    {
        var results =
            await _repository.GetExamResultsAsync(examId);

        return results.Select(x => new ResultResponseDTO
        {
            ResultId = x.ResultId,
            StudentId = x.StudentId,
            ExamId = x.ExamId,
            MarksObtained = x.MarksObtained??0,
            Grade = x.Grade,
            Remarks = x.Remarks
        }).ToList();
    }
}