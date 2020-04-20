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

    public class EmployerServiceTests
    {
        [Fact]
        public void GetEmployerCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Employer>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<Employer>
                {
                    new Employer(),
                    new Employer(),
                    new Employer(),
                }.AsQueryable());

            var service = new EmployerService(repository.Object, null);
            Assert.Equal(3, service.GetEmployerCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetEmployerCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployerTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Employers.Add(new Employer());
            dbContext.Employers.Add(new Employer());
            dbContext.Employers.Add(new Employer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employer>(dbContext);
            var service = new EmployerService(repository, null);
            Assert.Equal(3, service.GetEmployerCount());
        }

        [Fact]
        public void GetByIdShouldReturnEmployerModel()
        {
            string name = "Employer Name";
            string eik = "0000000000";

            var expected = new Employer { Id = 1, Name = name, Eik = eik };
            var repository = new Mock<IDeletableEntityRepository<Employer>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<Employer>
                {
                    new Employer() { Id = 1, Name = name, Eik = eik },
                }.AsQueryable());

            var service = new EmployerService(repository.Object, null);
            var result = service.GetById(1);
            Assert.True(
                expected.Id == result.Id &&
                expected.Name == result.Name &&
                expected.Eik == result.Eik);
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task ContainPostShouldReturnTrue()
        {
            Employer employer = new Employer();
            employer.JobPosts.Add(new JobPost() { Id = 1 });

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployerContainPostTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Employers.Add(employer);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employer>(dbContext);
            var service = new EmployerService(repository, null);
            Assert.True(service.ContaintPost(1, 1));
        }

        [Fact]
        public async Task ContainPostShouldReturnFalse()
        {
            Employer employer = new Employer();
            employer.JobPosts.Add(new JobPost() { Id = 1 });

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployerNotContainPostTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Employers.Add(employer);
            dbContext.Employers.Add(new Employer());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employer>(dbContext);
            var service = new EmployerService(repository, null);
            Assert.False(service.ContaintPost(1, 2));
        }

        [Fact]
        public async Task EditShouldEditEmployer()
        {
            string description = "Test Description";
            string city = "City";
            string country = "Country";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployerEditDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Add(new Employer
            {
                Description = description,
                City = city,
                Country = country,
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Employer>(dbContext);
            var service = new EmployerService(repository, null);

            var expected = new Employer
            {
                Description = description + "edit",
                City = city + "edit",
                Country = country + "edit",
            };

            await service.EditAsync(
                1,
                city + "edit",
                country + "edit",
                description + "edit",
                null);
            var result = service.GetById(1);

            Assert.True(
               expected.Description == result.Description &&
               expected.City == result.City &&
               expected.Country == result.Country);
        }
    }
}
