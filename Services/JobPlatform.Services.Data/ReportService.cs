namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;

    public class ReportService : IReportService
    {
        private readonly IDeletableEntityRepository<Report> reportRepository;

        public ReportService(IDeletableEntityRepository<Report> reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task CreateAsync(string title, string message, int? postId, string userId)
        {
            if (postId == null && userId == null)
            {
                return;
            }

            Report report = new Report
            {
                Title = title,
                Message = message,
                JobPostId = postId.HasValue ? postId : null,
                ApplicationUserId = userId,
            };

            await this.reportRepository.AddAsync(report);
            await this.reportRepository.SaveChangesAsync();
            return;
        }
    }
}
