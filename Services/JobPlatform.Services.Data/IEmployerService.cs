namespace JobPlatform.Services.Data
{
    using JobPlatform.Data.Models;
    using System.Threading.Tasks;

    public interface IEmployerService
    {
        Employer GetById(int id);

        T GetById<T>(int id);

        Task EditAsync(int id, string city, string country, string description);
    }
}
