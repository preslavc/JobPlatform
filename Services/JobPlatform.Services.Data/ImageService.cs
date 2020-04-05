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

    public class ImageService : IImageService
    {
        private readonly IConfiguration configuration;
        private readonly IStringManipulationService stringManipulationService;

        public ImageService(
            IConfiguration configuration,
            IStringManipulationService stringManipulationService)
        {
            this.configuration = configuration;
            this.stringManipulationService = stringManipulationService;
        }

        public async Task<string> UploadImage(IFormFile file, string employer)
        {
            string path = GlobalConstants.DropBoxImageUrl + employer + ".png";
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

                return this.stringManipulationService.CreateDropBoxImageUrl(sharedLinkMetadata.Url);
            }
        }
    }
}
