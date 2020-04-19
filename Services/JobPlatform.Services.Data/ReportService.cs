namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Common;
    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

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

        public async Task UpdateAsync(int reportId, bool status, string message)
        {
            Report report = this.GetReport(reportId);
            report.Resolved = status;
            report.ResolvedInfo = message;
            this.reportRepository.Update(report);
            await this.reportRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllPostReports<T>(int? page = null)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            IQueryable<Report> query = this.reportRepository.All()
                .Where(x => x.Resolved == false)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(((int)page - 1) * GlobalConstants.ItemsPerPage)
                .Take(GlobalConstants.ItemsPerPage);

            return query.To<T>().ToList();
        }

        public double GetReportCount()
        {
            return this.reportRepository.All()
               .Where(x => x.Resolved == false)
               .Count();
        }

        public T GetById<T>(int id)
        {
            return this.reportRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public Report GetReport(int id)
        {
            return this.reportRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}
