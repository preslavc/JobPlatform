namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    public interface ITagService
    {
        Task AddAsync(int jobPostId, string tagNames);
    }
}
