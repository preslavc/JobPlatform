namespace JobPlatform.Services.Data
{
    using System.Linq;

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

        public T GetById<T>(int id)
        {
            return this.jobPostsRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
