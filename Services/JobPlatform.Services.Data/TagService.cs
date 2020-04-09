namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class TagService : ITagService
    {
        private readonly IRepository<JobTag> jobTagRepository;
        private readonly IDeletableEntityRepository<Tag> tagRepository;

        public TagService(
            IRepository<JobTag> jobTagRepository,
            IDeletableEntityRepository<Tag> tagRepository)
        {
            this.jobTagRepository = jobTagRepository;
            this.tagRepository = tagRepository;
        }

        public async Task AddAsync(int jobPostId, string tagNames)
        {
            if (string.IsNullOrEmpty(tagNames))
            {
                return;
            }

            string[] tags = tagNames.ToLower().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in tags)
            {
                Tag tag = await this.GetOrCreateAsync(t);
                await this.jobTagRepository.AddAsync(new JobTag
                {
                    JobPostId = jobPostId,
                    TagId = tag.Id,
                });
            }

            await this.jobTagRepository.SaveChangesAsync();
            return;
        }

        public async Task UpdateTagAsync(int jobPostId, string tagNames)
        {
            var jobTags = this.jobTagRepository.All().Where(x => x.JobPostId == jobPostId);
            if (jobTags.Count() > 0)
            {
                foreach (var jobTag in jobTags)
                {
                    this.jobTagRepository.Delete(jobTag);
                }

                await this.jobTagRepository.SaveChangesAsync();
            }

            await this.AddAsync(jobPostId, tagNames);
        }

        public string GetTagToString(int jobId)
        {
            var tagsFromJob = this.tagRepository.All().Where(x => x.JobPosts.Any(x => x.JobPostId == jobId)).ToArray();
            string tags = string.Empty;
            foreach (var tag in tagsFromJob)
            {
                tags += tag.Name + " ";
            }

            return tags;
        }

        private async Task<Tag> GetOrCreateAsync(string name)
        {
            Tag tag = this.tagRepository.All()
                .Where(x => x.Name == name)
                .FirstOrDefault();
            if (tag == null)
            {
                tag = new Tag
                {
                    Name = name,
                };
                await this.tagRepository.AddAsync(tag);
                await this.tagRepository.SaveChangesAsync();
            }

            return tag;
        }
    }
}
