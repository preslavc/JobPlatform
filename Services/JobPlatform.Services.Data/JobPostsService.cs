namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class JobPostsService : IJobPostsService
    {
        private readonly IDeletableEntityRepository<JobPost> jobPostsRepository;

        public JobPostsService(IDeletableEntityRepository<JobPost> jobPostsRepository)
        {
            this.jobPostsRepository = jobPostsRepository;
        }

        public async Task<int> CreateAsync(string title, string description, string city, string country, int employerId)
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

        public async Task EditAsync(int id, string title, string description, string city, string country)
        {
            JobPost jobPost = this.GetJobPost(id);
            jobPost.Title = title;
            jobPost.Description = description;
            jobPost.City = city;
            jobPost.Country = country;

            this.jobPostsRepository.Update(jobPost);
            await this.jobPostsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<JobPost> query = this.jobPostsRepository.All().OrderByDescending(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public JobPost GetJobPost(int id)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}
