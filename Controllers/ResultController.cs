using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.DTOS.Exam_ResultDtos;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/results")]
[ApiVersion("1.0")]
public class ResultController : ControllerBase
{
    private readonly IResultService _service;

    public ResultController(IResultService service)
    {
        _service = service;
    }
    //========================================================
    [HttpPost]
    [Authorize(Roles = "TEACHER")]
    [SwaggerOperation(Summary = "Publish Result",Description = "Publish student examination result")]
    public async Task<IActionResult>PublishResult(PublishResultRequestDTO dto)
    {
        await _service.PublishResultAsync(dto);
        return Ok();
    }
    //===================================================

    [HttpGet("student/{studentId}")]
    [Authorize(Roles = "ADMIN,TEACHER,STUDENT")]
    [SwaggerOperation(Summary = "see the result", Description = "student examination results")]
    [ProducesResponseType(typeof(List<ResultResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStudentResults(int studentId)
    {
        var results = await _service.GetStudentResultsAsync(studentId);

        return Ok(results);
    }
    //=======================================================

    [HttpGet("exam/{examId}")]
    [Authorize(Roles = "ADMIN,TEACHER")]
    [SwaggerOperation(Summary = "results by using examid", Description = " student examination result")]
    [ProducesResponseType(typeof(List<ResultResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExamResults(int examId)
    {
        var results = await _service.GetExamResultsAsync(examId);

        return Ok(results);
    }

}