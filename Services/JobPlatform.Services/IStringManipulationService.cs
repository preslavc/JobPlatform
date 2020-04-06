namespace JobPlatform.Services
{
    public interface IStringManipulationService
    {
        string CreateDropBoxImageUrl(string imageUrl);

        string CreateDropBoxCvUrl(string cvUrl);
    }
}
