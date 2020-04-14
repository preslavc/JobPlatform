namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    public interface IReportService
    {
        Task CreateAsync(string title, string message, int? postId, string userId);
    }
}
