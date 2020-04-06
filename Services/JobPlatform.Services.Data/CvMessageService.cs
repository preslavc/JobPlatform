namespace JobPlatform.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
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

        public async Task SendCvAsync(ApplicationUser user, int jobPostId, string message, IFormFile file)
        {
            CvMessage cvmessage = new CvMessage
            {
                ApplicationUserId = user.Id,
                JobPostId = jobPostId,
                Message = message,
                CvUrl = await this.fileUploadService.UploadCvAsync(file, user.FirstName + user.LastName),
            };
            await this.cvмessageRepository.AddAsync(cvmessage);
            await this.cvмessageRepository.SaveChangesAsync();
        }
    }
}
