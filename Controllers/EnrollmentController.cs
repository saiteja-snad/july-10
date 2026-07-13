using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.DTOS.EnrollmentDtos;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/enrollments")]
[ApiVersion("1.0")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _service;

    public EnrollmentController(IEnrollmentService service)
    {
        _service = service;
    }
    //======================================
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Enroll Student", Description = "Enroll a student into a course")]
    public async Task<IActionResult>CreateEnrollment(CreateEnrollmentRequestDTO dto)
    {
        await _service.CreateEnrollmentAsync(dto);
        return Ok();
    }

    //=============================================================

    [HttpGet("{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Enroll Student  details", Description = "details of enrollment")]
    public async Task<IActionResult> GetEnrollmentById(int id)
    {
        var enrollment = await _service.GetEnrollmentByIdAsync(id);

        if (enrollment == null)
            return NotFound("Enrollment not found.");

        return Ok(enrollment);
    }
    //================================================

    // Get Enrollments By Student Id
    [HttpGet("student/{studentId}")]
    [Authorize]
    [SwaggerOperation(Summary = "student enrollment", Description = "details for student enrollment")]
    public async Task<IActionResult> GetStudentEnrollments(int studentId)
    {
        var enrollments =await _service.GetStudentEnrollmentsAsync(studentId);

        return Ok(enrollments);
    }
    //====================================================================

    // Get Enrollments By Course Id
    [HttpGet("course/{courseId}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "course details", Description = "Enroll a student into a course")]
    public async Task<IActionResult> GetCourseEnrollments(int courseId)
    {
        var enrollments =await _service.GetCourseEnrollmentsAsync(courseId);

        return Ok(enrollments);
    }
    //=======================================================================

    // Update Enrollment
    [HttpPut("{id}")]
    [Authorize(Roles="ADMIN")]
    [SwaggerOperation(Summary = " update  Student Enrollment", Description = "update a student enrollment")]
    public async Task<IActionResult> UpdateEnrollment(int id,CreateEnrollmentRequestDTO dto)
    {
        var updated =await _service.UpdateEnrollmentAsync(id, dto);

        if (!updated)
            return NotFound("Enrollment not found.");

        return Ok("Enrollment updated successfully.");
    }

}