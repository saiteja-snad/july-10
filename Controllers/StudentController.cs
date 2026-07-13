using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.DTOS.StudentDtos;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/students")]
[ApiVersion("1.0")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentController(IStudentService service)
    {
        _service = service;
    }
    //================================================================

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Create Student",Description = "Create new student profile")]
    [SwaggerResponse(201, "Student Created")]
    [SwaggerResponse(400, "Invalid Request")]
    public async Task<IActionResult> CreateStudent(CreateStudentRequestDTO dto)
    {
        var result = await _service.CreateStudentAsync(dto);
        return CreatedAtAction(nameof(GetStudent), new { id = result.StudentId }, result);
    }
    //============================================================================

    [HttpGet]
    [Authorize(Roles = "ADMIN,STAFF")]
    [SwaggerOperation(Summary = "Get All Students",Description = "Retrieve all students")]
    [SwaggerResponse(200, "Students Retrieved")]
    public async Task<IActionResult> GetAllStudents()
    {
        return Ok(await _service.GetAllStudentsAsync());
    }
    //================================================================================

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get Student By Id",Description = "Retrieve single student details")]
    [SwaggerResponse(200, "Student Found")]
    [SwaggerResponse(404, "Student Not Found")]
    public async Task<IActionResult> GetStudent(int id)
    {
        return Ok(await _service.GetStudentByIdAsync(id));
    }
    //===========================================================================================
    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation( Summary = "Update Student",Description = "Update student profile")]
    public async Task<IActionResult> UpdateStudent(int id,UpdateStudentRequestDTO dto)
    {
        return Ok(await _service.UpdateStudentAsync(id, dto));
    }
}