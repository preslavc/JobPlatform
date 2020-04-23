namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Common;
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
                CvUrl = file == null ? string.Empty : await this.fileUploadService.UploadCvAsync(file, firstName + lastName),
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

        public IEnumerable<T> GetAllMessagesAsync<T>(string userId, int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            var query = this.cvмessageRepository.All()
                .Where(x => x.JobPost.Employer.ApplicationUser.Id == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(((int)page - 1) * GlobalConstants.ItemsPerPage)
                .Take(GlobalConstants.ItemsPerPage);

            return query.To<T>()
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

        public CvMessage GetMessage(int messegeId)
        {
            return this.cvмessageRepository.All()
                .Where(x => x.Id == messegeId)
                .FirstOrDefault();
        }

        public double GetMessageCount(string userId)
        {
            return this.cvмessageRepository.All()
                .Where(x => x.JobPost.Employer.ApplicationUser.Id == userId)
                .Count();
        }

        public double GetMessageCount(int postId)
        {
            return this.cvмessageRepository.All()
                .Where(x => x.JobPostId == postId)
                .Count();
        }

        public T GetById<T>(int messageId)
        {
            return this.cvмessageRepository.All()
                .Where(x => x.Id == messageId)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
