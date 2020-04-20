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
        public async Task JobCountByEmployerShouldReturnCorrectNumber()
        {
            Employer employer = new Employer { Name = "Test" };
            Employer employer2 = new Employer { Name = "Invalid employer" };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "JobPostByEmployerDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.JobPosts.Add(new JobPost() { Employer = employer });
            dbContext.JobPosts.Add(new JobPost() { Employer = employer });
            dbContext.JobPosts.Add(new JobPost() { Employer = employer2 });
            dbContext.JobPosts.Add(new JobPost());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<JobPost>(dbContext);
            var service = new JobPostsService(repository, null);
            Assert.Equal(2, service.GetJobCountByEmployer("Test"));
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
        public void GetJobPostShouldReturnJobPostModel()
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
        public async Task CreateShouldCreateJobPost()
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

        [Fact]
        public async Task EditShouldEditJobPost()
        {
            string title = "Test 1";
            string description = "Test Description";
            string city = "City";
            string country = "Country";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "JobEditDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<JobPost>(dbContext);
            var tagRepository = new Mock<ITagService>();
            var service = new JobPostsService(repository, tagRepository.Object);

            var id = await service.CreateAsync(title, description, city, country, 1, string.Empty);

            var expected = new JobPost
            {
                Title = title + "edit",
                Description = description + "edit",
                City = city + "edit",
                Country = country + "edit",
                EmployerId = 1,
            };

            await service.EditAsync(
                id,
                title + "edit",
                description + "edit",
                city + "edit",
                country + "edit",
                string.Empty);
            var result = service.GetJobPost(id);

            Assert.True(
               expected.Title == result.Title &&
               expected.Description == result.Description &&
               expected.City == result.City &&
               expected.Country == result.Country &&
               expected.EmployerId == result.EmployerId);
        }

        [Fact]
        public async Task DeleteShouldDeleteJobPost()
        {
            JobPost jobPost = new JobPost()
            {
                Id = 1,
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "JobDeleteDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.JobPosts.Add(jobPost);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<JobPost>(dbContext);
            var service = new JobPostsService(repository, null);
            Assert.Equal(1, service.GetJobCount());
            await service.DeleteAsync(jobPost);
            Assert.Equal(0, service.GetJobCount());
        }
    }
}
