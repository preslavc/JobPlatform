namespace JobPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CvMessageService : ICvMessageService
    {
        private readonly IDeletableEntityRepository<CvMessage> cvмessageRepository;
        private readonly IFileUploadService fileUploadService;

        public CvMessageService(
            IDeletableEntityRepository<CvMessage> cvмessageRepository,
            IFileUploadService fileUploadService)
        {
            this.cvмessageRepository = cvмessageRepository;
            this.fileUploadService = fileUploadService;
        }

        public async Task SendCvAsync(ApplicationUser user, int jobPostId, string message, IFormFile file, string firstName, string lastName)
        {
            CvMessage cvmessage = new CvMessage
            {
                ApplicationUserId = user.Id,
                FirstName = firstName,
                LastName = lastName,
                JobPostId = jobPostId,
                Message = message,
                CvUrl = await this.fileUploadService.UploadCvAsync(file, user.FirstName + user.LastName),
            };
            await this.cvмessageRepository.AddAsync(cvmessage);
            await this.cvмessageRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllMessagesAsync<T>(int postId)
        {
            return this.cvмessageRepository.All()
                .Where(x => x.JobPostId == postId)
                .To<T>()
                .ToList();
        }

        public async Task DeleteAsync(CvMessage cvmessage)
        {
            if (cvmessage == null)
            {
                return;
            }

            this.cvмessageRepository.Delete(cvmessage);
            await this.cvмessageRepository.SaveChangesAsync();
            return;
        }

        public CvMessage GetMessages(int messegeId)
        {
            return this.cvмessageRepository.All()
                .Where(x => x.Id == messegeId)
                .FirstOrDefault();
        }
    }
}
