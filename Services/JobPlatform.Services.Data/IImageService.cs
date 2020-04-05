namespace JobPlatform.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        public Task<string> UploadImage(IFormFile file, string employer);
    }
}
