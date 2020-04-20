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

    public class CvMessageServiceTests
    {
        [Fact]
        public void GetMessageCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<CvMessage>>();
            repository.Setup(posts => posts.All())
                .Returns(new List<CvMessage>
                {
                    new CvMessage() { Id = 1, JobPostId = 1 },
                    new CvMessage() { Id = 2, JobPostId = 1 },
                    new CvMessage() { Id = 3, JobPostId = 2 },
                }.AsQueryable());

            var service = new CvMessageService(repository.Object, null);
            Assert.Equal(2, service.GetMessageCount(1));
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetMessageCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CvMessageCountTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.CvMessages.Add(new CvMessage() { JobPostId = 1 });
            dbContext.CvMessages.Add(new CvMessage() { JobPostId = 1 });
            dbContext.CvMessages.Add(new CvMessage() { JobPostId = 2 });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<CvMessage>(dbContext);
            var service = new CvMessageService(repository, null);
            Assert.Equal(1, service.GetMessageCount(2));
        }

        [Fact]
        public async Task SendCvAsyncShouldSendMessageToEmployer()
        {
            ApplicationUser user = new ApplicationUser { Id = "TestId" };
            string message = "TestMessage";
            string firstName = "FirstNameTest";
            string lastName = "LastNameTest";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SendCVCreateDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<CvMessage>(dbContext);
            var service = new CvMessageService(repository, null);

            await service.SendCvAsync(user, 1, message, null, firstName, lastName);

            var messageCount = service.GetMessageCount(1);
            Assert.Equal(1, messageCount);

            var expected = new CvMessage
            {
                Message = message,
                FirstName = firstName,
                LastName = lastName,
            };
            var result = service.GetMessage(1);

            Assert.True(
               expected.Message == result.Message &&
               expected.FirstName == result.FirstName &&
               expected.LastName == result.LastName);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteMessage()
        {
            int postId = 1;

            CvMessage cvmessage = new CvMessage
            {
                JobPostId = postId,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteCvDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.CvMessages.Add(cvmessage);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<CvMessage>(dbContext);
            var service = new CvMessageService(repository, null);
            var messageCount = service.GetMessageCount(1);
            Assert.Equal(1, messageCount);
            var result = service.DeleteAsync(cvmessage);
            messageCount = service.GetMessageCount(1);
            Assert.Equal(0, messageCount);
        }
    }
}
