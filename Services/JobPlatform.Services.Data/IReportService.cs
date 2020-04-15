namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReportService
    {
        Task CreateAsync(string title, string message, int? postId, string userId);

        IEnumerable<T> GetAllPostReports<T>(int? page = null);
    }
}
