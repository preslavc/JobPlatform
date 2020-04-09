namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    public interface ITagService
    {
        Task AddAsync(int jobPostId, string tagNames);

        string GetTagToString(int jobId);

        Task UpdateTagAsync(int jobPostId, string tagNames);
    }
}
