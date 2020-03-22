namespace JobPlatform.Services.Data
{
    using System.Linq;

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

        public T GetById<T>(int id)
        {
            return this.employerRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
