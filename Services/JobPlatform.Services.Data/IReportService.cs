namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;

    public interface IReportService
    {
        Task CreateAsync(string title, string message, int? postId);

        Task UpdateAsync(int reportId, bool status, string message);

        IEnumerable<T> GetAllPostReports<T>(int? page = null);

        double GetReportCount();

        T GetById<T>(int id);

        Report GetReport(int id);
    }
}
