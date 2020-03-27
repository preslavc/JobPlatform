namespace JobPlatform.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class EmployerService : IEmployerService
    {
        private readonly IDeletableEntityRepository<Employer> employerRepository;

        public EmployerService(IDeletableEntityRepository<Employer> employerRepository)
        {
            this.employerRepository = employerRepository;
        }

        public Employer GetById(int id)
        {
            return this.employerRepository.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public T GetById<T>(int id)
        {
            return this.employerRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task EditAsync(int id, string city, string country, string description)
        {
            Employer employer = this.GetById(id);
            employer.City = city;
            employer.Country = country;
            employer.Description = description;

            this.employerRepository.Update(employer);
            await this.employerRepository.SaveChangesAsync();
        }
    }
}
