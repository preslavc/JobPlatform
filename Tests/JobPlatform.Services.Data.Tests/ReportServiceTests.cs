namespace JobPlatform.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Data;
    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ReportServiceTests
    {
        [Fact]
        public void GetReportCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Report>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<Report>
                {
                    new Report(),
                    new Report(),
                    new Report(),
                }.AsQueryable());

            var service = new ReportService(repository.Object);
            Assert.Equal(3, service.GetReportCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetReportCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReportCountTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Reports.Add(new Report());
            dbContext.Reports.Add(new Report());
            dbContext.Reports.Add(new Report());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Report>(dbContext);
            var service = new ReportService(repository);
            Assert.Equal(3, service.GetReportCount());
        }

        [Fact]
        public void GetReportShouldReturnReportModel()
        {
            string message = "Test report message";

            var expected = new Report { Id = 1, Message = message, };
            var repository = new Mock<IDeletableEntityRepository<Report>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<Report>
                {
                    new Report() { Id = 1, Message = message, },
                }.AsQueryable());

            var service = new ReportService(repository.Object);
            var result = service.GetReport(1);
            Assert.True(
                expected.Id == result.Id &&
                expected.Title == result.Title);
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateReport()
        {
            string title = "Test 1";
            string message = "Test message";
            int postId = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReportCreateDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Report>(dbContext);
            var service = new ReportService(repository);

            await service.CreateAsync(title, message, postId);

            var reportsCount = service.GetReportCount();
            Assert.Equal(1, reportsCount);
            var expected = new Report
            {
                Title = title,
                Message = message,
                JobPostId = postId,
            };
            var result = service.GetReport(1);

            Assert.True(
               expected.Title == result.Title &&
               expected.Message == result.Message &&
               expected.JobPostId == result.JobPostId &&
               expected.Resolved == result.Resolved);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateReport()
        {
            string message = "ResolveInfo Test";
            bool status = true;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReportUpdateDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Reports.Add(new Report());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Report>(dbContext);
            var service = new ReportService(repository);

            await service.UpdateAsync(1, status, message);

            var expected = new Report
            {
                Resolved = status,
                ResolvedInfo = message,
            };
            var result = service.GetReport(1);

            Assert.True(
               expected.Resolved == result.Resolved &&
               expected.ResolvedInfo == result.ResolvedInfo);
        }
    }
}
