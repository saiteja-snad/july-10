namespace StudentManagementSystem.Services.Interfaces
{
    public interface IReportService
    {
        Task<object> GetAttendanceSummaryAsync();

        Task<object> GetFeeDueReportAsync();

        Task<object> GetPerformanceReportAsync();
    }
}