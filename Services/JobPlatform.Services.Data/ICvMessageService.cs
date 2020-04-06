namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface ICvMessageService
    {
        public Task SendCvAsync(ApplicationUser user, int jobPostId, string message, IFormFile file);
    }
}
