namespace JobPlatform.Services
{
    public class StringManipulationService : IStringManipulationService
    {
        public string CreateDropBoxImageUrl(string imageUrl)
        {
            return imageUrl.Replace("dl=0", "raw=1");
        }
    }
}
