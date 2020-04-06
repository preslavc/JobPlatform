namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileUploadService
    {
        public Task<string> UploadImageAsync(IFormFile file, string employer);

        public Task<string> UploadCvAsync(IFormFile file, string user);
    }
}
