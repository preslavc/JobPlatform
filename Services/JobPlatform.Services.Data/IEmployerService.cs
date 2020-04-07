namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IEmployerService
    {
        Employer GetById(int id);

        T GetById<T>(int id);

        Task EditAsync(int id, string city, string country, string description, IFormFile image);

        bool ContaintPost(int postId, int employerId);
    }
}
