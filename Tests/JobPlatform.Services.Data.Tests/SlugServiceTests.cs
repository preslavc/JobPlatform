namespace JobPlatform.Services.Data.Tests
{
    using Xunit;

    public class SlugServiceTests
    {
        [Fact]
        public void SlugLongTextTest()
        {
            string input = "LongTextInput4567892123456789312345678941234567895123456789612345678971234567898123456789912345678901";
            ISlugService slugService = new SlugService();
            string result = slugService.ConvertSlug(input);
            Assert.True(result.Length <= 100);
        }

        [Fact]
        public void ConvertBulgarianText()
        {
            string input = "Частна обява: Позиция - \"Продуктов мениджър\"";
            string expected = "chastna-obyava-poziciya-produktov-menidjur";
            ISlugService slugService = new SlugService();
            string result = slugService.ConvertSlug(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SymbolsText()
        {
            string input = "W@e#b$ --D(e)v-e=l+o;p:e?r ";
            string expected = "web-dev-eloper";
            ISlugService slugService = new SlugService();
            string result = slugService.ConvertSlug(input);
            Assert.Equal(expected, result);
        }
    }
}
