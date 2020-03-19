namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    public interface IJobPostsService
    {
        Task<int> CreateAsync(string title, string description, string city, string country, int employerId);

        T GetById<T>(int id);
    }
}
