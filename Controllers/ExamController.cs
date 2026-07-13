using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.DTOS.Exam_ResultDtos;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/exams")]
[ApiVersion("1.0")]
public class ExamController : ControllerBase
{
    private readonly IExamService _service;

    public ExamController(IExamService service)
    {
        _service = service;
    }
    //===============================================
    [HttpPost]
    [Authorize(Roles = "ADMIN,TEACHER")]
    [SwaggerOperation(Summary = "Create Exam", Description = "Create examination record")]
    public async Task<IActionResult>CreateExam(CreateExamRequestDTO dto)
    {
        await _service.CreateExamAsync(dto);
        return Ok();
    }

    //========================

    [HttpGet]
    [Authorize(Roles = "ADMIN,TEACHER,STUDENT")]
    public async Task<IActionResult> GetAllExams()
    {
        var exams = await _service.GetAllExamsAsync();

        return Ok(exams);
    }
}