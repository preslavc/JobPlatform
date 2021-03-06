﻿namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Common;
    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class JobPostsService : IJobPostsService
    {
        private readonly IDeletableEntityRepository<JobPost> jobPostsRepository;
        private readonly ITagService tagService;

        public JobPostsService(
            IDeletableEntityRepository<JobPost> jobPostsRepository,
            ITagService tagService)
        {
            this.jobPostsRepository = jobPostsRepository;
            this.tagService = tagService;
        }

        public async Task<int> CreateAsync(string title, string description, string city, string country, int employerId, string tags)
        {
            JobPost jobPost = new JobPost
            {
                Title = title,
                Description = description,
                City = city,
                Country = country,
                EmployerId = employerId,
            };
            await this.jobPostsRepository.AddAsync(jobPost);
            await this.jobPostsRepository.SaveChangesAsync();
            await this.tagService.AddAsync(jobPost.Id, tags);
            return jobPost.Id;
        }

        public async Task DeleteAsync(JobPost jobPost)
        {
            if (jobPost == null)
            {
                return;
            }

            this.jobPostsRepository.Delete(jobPost);
            await this.jobPostsRepository.SaveChangesAsync();
            return;
        }

        public async Task EditAsync(int id, string title, string description, string city, string country, string tags)
        {
            JobPost jobPost = this.GetJobPost(id);
            jobPost.Title = title;
            jobPost.Description = description;
            jobPost.City = city;
            jobPost.Country = country;

            this.jobPostsRepository.Update(jobPost);
            await this.jobPostsRepository.SaveChangesAsync();
            await this.tagService.UpdateTagAsync(id, tags);
        }

        public IEnumerable<T> GetAll<T>(int? page = null)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            IQueryable<JobPost> query = this.jobPostsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(((int)page - 1) * GlobalConstants.ItemsPerPage)
                .Take(GlobalConstants.ItemsPerPage);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllBy<T>(string keyword = null, string city = null, int? page = null)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            IQueryable<JobPost> query = this.jobPostsRepository.All()
                .Where(x => x.Title.Contains(keyword) || x.Tags.Any(t => t.Tag.Name == keyword) || x.City == city)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(((int)page - 1) * GlobalConstants.ItemsPerPage)
                .Take(GlobalConstants.ItemsPerPage);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByEmployer<T>(string name, int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            IQueryable<JobPost> query = this.jobPostsRepository.All()
                .Where(x => x.Employer.Name == name)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(((int)page - 1) * GlobalConstants.ItemsPerPage)
                .Take(GlobalConstants.ItemsPerPage);
            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByTag<T>(string keyword, int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            IQueryable<JobPost> query = this.jobPostsRepository.All()
                .Where(x => x.Tags.Any(t => t.Tag.Name == keyword))
                .OrderByDescending(x => x.CreatedOn)
                .Skip(((int)page - 1) * GlobalConstants.ItemsPerPage)
                .Take(GlobalConstants.ItemsPerPage);
            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public double GetJobCount()
        {
            return this.jobPostsRepository.All().Count();
        }

        public double GetJobCountByTag(string keyword)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Tags.Any(t => t.Tag.Name == keyword))
                .Count();
        }

        public double GetJobCountBySearch(string keyword, string city)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Title.Contains(keyword) || x.Tags.Any(t => t.Tag.Name == keyword) || x.City == city)
                .Count();
        }

        public double GetJobCountByEmployer(string name)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Employer.Name == name)
                .Count();
        }

        public JobPost GetJobPost(int id)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public bool JobPostExist(int jobPostId)
        {
            JobPost jobPost = this.GetJobPost(jobPostId);
            if (jobPost == null)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteAllPostsFromEmployer(int employerId)
        {
            IEnumerable<JobPost> jobPosts = this.jobPostsRepository.All().Where(x => x.EmployerId == employerId);
            if (jobPosts == null)
            {
                return;
            }

            foreach (var job in jobPosts)
            {
                this.jobPostsRepository.Delete(job);
            }

            await this.jobPostsRepository.SaveChangesAsync();
        }
    }
}
