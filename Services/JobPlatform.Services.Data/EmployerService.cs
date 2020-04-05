namespace JobPlatform.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services;
    using JobPlatform.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EmployerService : IEmployerService
    {
        private readonly IDeletableEntityRepository<Employer> employerRepository;
        private readonly IStringManipulationService stringManipulationService;
        private readonly IImageService imageService;

        public EmployerService(
            IDeletableEntityRepository<Employer> employerRepository,
            IStringManipulationService stringManipulationService,
            IImageService imageService)
        {
            this.employerRepository = employerRepository;
            this.stringManipulationService = stringManipulationService;
            this.imageService = imageService;
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

        public async Task EditAsync(int id, string city, string country, string description, IFormFile image)
        {
            Employer employer = this.GetById(id);
            employer.City = city;
            employer.Country = country;
            employer.Description = description;
            if (image != null)
            {
                employer.ImageUrl = await this.imageService.UploadImage(image, employer.Name);
            }

            this.employerRepository.Update(employer);
            await this.employerRepository.SaveChangesAsync();
        }
    }
}
