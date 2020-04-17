namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface ICvMessageService
    {
        public Task SendCvAsync(ApplicationUser user, int jobPostId, string message, IFormFile file, string firstName, string lastName);

        IEnumerable<T> GetAllMessagesAsync<T>(int postId);

        IEnumerable<T> GetAllMessagesAsync<T>(string userId, int? page);

        Task DeleteAsync(CvMessage cvmessage);

        CvMessage GetMessages(int messegeId);

        double GetMessageCount(string userId);

        double GetMessageCount(int postId);
    }
}
