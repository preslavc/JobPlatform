﻿namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Common;
    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EmployerService : IEmployerService
    {
        private readonly IDeletableEntityRepository<Employer> employerRepository;
        private readonly IFileUploadService imageService;

        public EmployerService(
            IDeletableEntityRepository<Employer> employerRepository,
            IFileUploadService imageService)
        {
            this.employerRepository = employerRepository;
            this.imageService = imageService;
        }

        public IEnumerable<T> GetAll<T>(int? page = null)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            IQueryable<Employer> query = this.employerRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(((int)page - 1) * GlobalConstants.ItemsPerPage)
                .Take(GlobalConstants.ItemsPerPage);

            return query.To<T>().ToList();
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
                employer.ImageUrl = await this.imageService.UploadImageAsync(image, employer.Name);
            }

            this.employerRepository.Update(employer);
            await this.employerRepository.SaveChangesAsync();
        }

        public bool ContaintPost(int postId, int employerId)
        {
            Employer employer = this.GetById(employerId);
            return employer.JobPosts.Any(x => x.Id == postId);
        }

        public double GetEmployerCount()
        {
            return this.employerRepository.All().Count();
        }

        public async Task DeleteEmployer(int employerId)
        {
            Employer employer = this.GetById(employerId);
            if (employer == null)
            {
                return;
            }

            this.employerRepository.Delete(employer);
            await this.employerRepository.SaveChangesAsync();
        }
    }
}
