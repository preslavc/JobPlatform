namespace JobPlatform.Services
{
    public class StringManipulationService : IStringManipulationService
    {
        public string CreateDropBoxCvUrl(string cvUrl)
        {
            return cvUrl.Replace("dl=0", "dl=1");
        }

        public string CreateDropBoxImageUrl(string imageUrl)
        {
            imageUrl = imageUrl.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
            return imageUrl.Replace("dl=0", "raw=1");
        }
    }
}
