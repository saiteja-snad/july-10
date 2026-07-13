using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/reports")]
[Authorize(Roles = "ADMIN")]
[ApiVersion("1.0")]
public class ReportController : ControllerBase
{
    private readonly IReportService _service;

    public ReportController(IReportService service )
    {
        _service = service;
    }
    //=============================================================
    [HttpGet("attendance-summary")]
    [SwaggerOperation(Summary = "Attendance Report", Description = "Generate attendance summary report")]
    public async Task<IActionResult>AttendanceSummary()
    {
        return Ok(await _service.GetAttendanceSummaryAsync());
    }
    //==========================================================================
    [HttpGet("fee-dues")]
    [SwaggerOperation(Summary = "Fee Due Report",Description = "Generate fee due report")]
    public async Task<IActionResult>FeeDueReport()
    {
        return Ok(await _service.GetFeeDueReportAsync());
    }
    //===================================================================================

    [HttpGet("performance-summary")]
    [SwaggerOperation(Summary = "Performance Report",Description = "Generate academic performance report")]
    public async Task<IActionResult>PerformanceSummary()
    {
        return Ok(await _service.GetPerformanceReportAsync());
    }
}