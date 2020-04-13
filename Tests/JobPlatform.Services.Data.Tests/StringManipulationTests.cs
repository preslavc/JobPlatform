namespace JobPlatform.Services.Data.Tests
{
    using Xunit;

    public class StringManipulationTests
    {
        [Fact]
        public void ConvertDropBoxImageUrl()
        {
            string input = "https://www.dropbox.com/s/0saydg9kep5ujsp/default-user-150x150.png?dl=0";
            string expected = "https://dl.dropboxusercontent.com/s/0saydg9kep5ujsp/default-user-150x150.png?raw=1";
            IStringManipulationService stringManipulationService = new StringManipulationService();
            string result = stringManipulationService.CreateDropBoxImageUrl(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertDropBoxCvDownloadUrl()
        {
            string input = "https://www.dropbox.com/s/0saydg9kep5ujsp/default-user-150x150.png?dl=0";
            string expected = "https://www.dropbox.com/s/0saydg9kep5ujsp/default-user-150x150.png?dl=1";
            IStringManipulationService stringManipulationService = new StringManipulationService();
            string result = stringManipulationService.CreateDropBoxCvUrl(input);
            Assert.Equal(expected, result);
        }
    }
}
