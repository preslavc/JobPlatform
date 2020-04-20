namespace JobPlatform.Services.Data.Tests
{
    using System.Threading.Tasks;

    using JobPlatform.Data;
    using JobPlatform.Data.Models;
    using JobPlatform.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class TagServiceTests
    {
        [Fact]
        public async Task AddAsyncShouldAddTagToJobPost()
        {
            string tags = "javascript c#";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddTagDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.JobPosts.Add(new JobPost());
            await dbContext.SaveChangesAsync();

            var jobTagRepository = new EfRepository<JobTag>(dbContext);
            var tagRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TagService(jobTagRepository, tagRepository);

            await service.AddAsync(1, tags);
            var result = service.GetTagToString(1);
            Assert.Equal(tags, result);
        }

        [Fact]
        public async Task UpdateTagAsyncShouldUpdateTags()
        {
            string tags = "javascript c# web";
            string updateTags = "python programming";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateTagDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.JobPosts.Add(new JobPost());
            await dbContext.SaveChangesAsync();

            var jobTagRepository = new EfRepository<JobTag>(dbContext);
            var tagRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TagService(jobTagRepository, tagRepository);

            await service.AddAsync(1, tags);
            var result = service.GetTagToString(1);
            Assert.Equal(tags, result);

            await service.UpdateTagAsync(1, updateTags);
            result = service.GetTagToString(1);
            Assert.Equal(updateTags, result);
        }

        [Fact]
        public async Task UpdateTagAsyncShouldRemoveAllTags()
        {
            string tags = "javascript c# web";
            string deleteTags = string.Empty;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteTagDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.JobPosts.Add(new JobPost());
            await dbContext.SaveChangesAsync();

            var jobTagRepository = new EfRepository<JobTag>(dbContext);
            var tagRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TagService(jobTagRepository, tagRepository);

            await service.AddAsync(1, tags);
            var result = service.GetTagToString(1);
            Assert.Equal(tags, result);

            await service.UpdateTagAsync(1, deleteTags);
            result = service.GetTagToString(1);
            Assert.Equal(deleteTags, result);
        }
    }
}
