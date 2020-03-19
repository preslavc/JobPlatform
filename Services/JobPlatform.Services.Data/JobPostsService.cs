namespace JobPlatform.Services.Data
{
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

        public T GetById<T>(int id)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
