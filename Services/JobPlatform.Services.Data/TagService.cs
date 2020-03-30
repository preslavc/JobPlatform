﻿namespace JobPlatform.Services.Data
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

        public IEnumerable<T> GetAll<T>(string tagName)
        {
            Tag tag = this.tagRepository.All().Where(x => x.Name == tagName).FirstOrDefault();
            if (tag == null)
            {
                return null;
            }

            IQueryable<JobTag> query = this.jobTagRepository.All().Where(x => x.TagId == tag.Id).OrderByDescending(x => x.CreatedOn);
            return query.To<T>().ToList();
        }

        private async Task<Tag> GetOrCreateAsync(string name)
        {
            Tag tag = this.tagRepository.All()
                .Where(x => x.Name == name)
                .FirstOrDefault();
            if (tag == null)
            {
                tag.Name = name;
                await this.tagRepository.AddAsync(tag);
                await this.tagRepository.SaveChangesAsync();
            }

            return tag;
        }
    }
}
