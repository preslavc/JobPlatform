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

    public class JobServiceTests
    {
        [Fact]
        public void JobCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<JobPost>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<JobPost>
                {
                    new JobPost(),
                    new JobPost(),
                    new JobPost(),
                }.AsQueryable());

            var service = new JobPostsService(repository.Object, null);
            Assert.Equal(3, service.GetJobCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task JobCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "JobPostTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.JobPosts.Add(new JobPost());
            dbContext.JobPosts.Add(new JobPost());
            dbContext.JobPosts.Add(new JobPost());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<JobPost>(dbContext);
            var service = new JobPostsService(repository, null);
            Assert.Equal(3, service.GetJobCount());
        }

        [Fact]
        public void JobPostShouldExist()
        {
            var repository = new Mock<IDeletableEntityRepository<JobPost>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<JobPost>
                {
                    new JobPost() { Id = 1, },
                    new JobPost() { Id = 2, },
                    new JobPost() { Id = 3, },
                }.AsQueryable());

            var service = new JobPostsService(repository.Object, null);
            Assert.True(service.JobPostExist(1));
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void JobPostShouldNotExist()
        {
            var repository = new Mock<IDeletableEntityRepository<JobPost>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<JobPost>
                {
                    new JobPost() { Id = 1, },
                    new JobPost() { Id = 2, },
                }.AsQueryable());

            var service = new JobPostsService(repository.Object, null);
            Assert.True(service.JobPostExist(2));
            Assert.False(service.JobPostExist(3));
            repository.Verify(x => x.All(), Times.Exactly(2));
        }

        [Fact]
        public void GetJobPostModelById()
        {
            var expected = new JobPost { Id = 1, Title = "Test 1"};
            var repository = new Mock<IDeletableEntityRepository<JobPost>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<JobPost>
                {
                    new JobPost() { Id = 1, Title = "Test 1" },
                }.AsQueryable());

            var service = new JobPostsService(repository.Object, null);
            var result = service.GetJobPost(1);
            Assert.True(
                expected.Id == result.Id &&
                expected.Title == result.Title);
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task CreateJobPost()
        {
            string title = "Test 1";
            string description = "Test Description";
            string city = "City";
            string country = "Country";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "JobPostCreateDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<JobPost>(dbContext);
            var tagRepository = new Mock<ITagService>();
            var service = new JobPostsService(repository, tagRepository.Object);

            var id = await service.CreateAsync(title, description, city, country, 1, string.Empty);
            Assert.Equal(1, id);

            var jobCount = service.GetJobCount();
            Assert.Equal(1, jobCount);

            var expected = new JobPost
            {
                Title = title,
                Description = description,
                City = city,
                Country = country,
                EmployerId = 1,
            };
            var result = service.GetJobPost(id);

            Assert.True(
               expected.Title == result.Title &&
               expected.Description == result.Description &&
               expected.City == result.City &&
               expected.Country == result.Country &&
               expected.EmployerId == result.EmployerId);
        }
    }
}
