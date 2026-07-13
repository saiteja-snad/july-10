using StudentManagementSystem.Services.Interfaces;

public class ReportService : IReportService
{
    public async Task<object>
        GetAttendanceSummaryAsync()
    {
        return await Task.FromResult(new { });
    }

    public async Task<object>
        GetFeeDueReportAsync()
    {
        return await Task.FromResult(new { });
    }

    public async Task<object>
        GetPerformanceReportAsync()
    {
        return await Task.FromResult(new { });
    }
}