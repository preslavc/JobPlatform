namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITagService
    {
        Task AddAsync(int jobPostId, string tagNames);

        IEnumerable<T> GetAll<T>(string tagName);
    }
}
