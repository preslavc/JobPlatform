namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;

    public interface IJobPostsService
    {
        Task<int> CreateAsync(string title, string description, string city, string country, int employerId);

        Task DeleteAsync(JobPost jobPost);

        JobPost GetJobPost(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
