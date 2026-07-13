using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.DTOS.AttendancedDtos;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/attendance")]
[ApiVersion("1.0")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService  _service;

    public AttendanceController(IAttendanceService service)
    {
        _service = service;
    }
    //============================================
    [HttpPost]
    [Authorize(Roles = "TEACHER,STAFF")]
    [SwaggerOperation(Summary = "Mark Attendance",Description = "Mark attendance for student")]
    public async Task<IActionResult> MarkAttendance(MarkAttendanceRequestDto dto)
    {
        await _service.MarkAttendanceAsync(dto);
        return Ok();
    }
    //====================================================

    [HttpGet("student/{studentId}")]
    [Authorize(Roles = "STUDENT,ADMIN")]
    [SwaggerOperation(Summary = "Student Attendance",Description = "Get student attendance history")]
    public async Task<IActionResult>GetStudentAttendance(int studentId)
    {
        return Ok(await _service.GetStudentAttendanceAsync(studentId));
    }
    //=====================================================

    [HttpGet("class/{classId}")]
    [Authorize(Roles = "TEACHER,ADMIN")]
    [SwaggerOperation(Summary = "Class Attendance",Description = "Get attendance sheet for entire class")]
    public async Task<IActionResult>GetClassAttendance(int classId)
    {
        return Ok(await _service.GetClassAttendanceAsync(classId));
    }
    //=======================================
    //[HttpPut("{id}")]
    //[Authorize(Roles = "TEACHER,STAFF")]
    //public async Task<IActionResult>
    //        UpdateAttendance(
    //        int id,
    //        UpdateAttendanceRequestDTO dto)
    //{
    //}

}