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

        IEnumerable<T> GetAll<T>(int? page = null);

        Task EditAsync(int id, string city, string country, string description, IFormFile image);

        bool ContaintPost(int postId, int employerId);

        double GetEmployerCount();

        Task DeleteEmployer(int employerId);
    }
}
