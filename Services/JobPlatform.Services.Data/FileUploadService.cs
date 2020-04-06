namespace JobPlatform.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Dropbox.Api;
    using Dropbox.Api.Files;
    using Dropbox.Api.Sharing;
    using JobPlatform.Common;
    using JobPlatform.Data.Models;
    using JobPlatform.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration configuration;
        private readonly IStringManipulationService stringManipulationService;

        public FileUploadService(
            IConfiguration configuration,
            IStringManipulationService stringManipulationService)
        {
            this.configuration = configuration;
            this.stringManipulationService = stringManipulationService;
        }

        public async Task<string> UploadCvAsync(IFormFile file, string user)
        {
            string path = GlobalConstants.DropBoxCvUrl + user + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
            string url = await this.FileUpload(file, path);
            return this.stringManipulationService.CreateDropBoxCvUrl(url);
        }

        public async Task<string> UploadImageAsync(IFormFile file, string employer)
        {
            string path = GlobalConstants.DropBoxImageUrl + employer + ".png";
            string url = await this.FileUpload(file, path);
            return this.stringManipulationService.CreateDropBoxImageUrl(url);
        }

        private async Task<string> FileUpload(IFormFile file, string path)
        {
            using (var dbx = new DropboxClient(this.configuration["DropBox:ApiKey"]))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    var updated = await dbx.Files.UploadAsync(
                        path,
                        WriteMode.Overwrite.Instance,
                        body: memoryStream);
                }

                SharedLinkMetadata sharedLinkMetadata;
                var linkSettings = new Dropbox.Api.Sharing.CreateSharedLinkWithSettingsArg(path);
                try
                {
                    sharedLinkMetadata = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(linkSettings);
                }
                catch (ApiException<CreateSharedLinkWithSettingsError> err)
                {
                    if (err.ErrorResponse.IsSharedLinkAlreadyExists)
                    {
                        var sharedLinksMetadata = await dbx.Sharing.ListSharedLinksAsync(path: path, cursor: null, directOnly: true);
                        sharedLinkMetadata = sharedLinksMetadata.Links.First();
                    }
                    else
                    {
                        throw err;
                    }
                }

                return sharedLinkMetadata.Url;
            }
        }
    }
}
