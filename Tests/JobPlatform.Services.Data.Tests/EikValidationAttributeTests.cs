namespace JobPlatform.Services.Data.Tests
{
    using JobPlatform.Web.Infrastructure.ValidationAttributes;
    using Xunit;

    public class EikValidationAttributeTests
    {
        [Theory]
        [InlineData("000000000")]
        [InlineData("160026043")]
        [InlineData("115904389")]
        [InlineData("400004390")]
        public void ValidEik(string eik)
        {
            EikValidationAttribute attribute = new EikValidationAttribute();
            bool value = attribute.IsValid(eik);
            Assert.True(value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1234as789")]
        [InlineData("abcdefghi")]
        [InlineData("         ")]
        [InlineData("\0")]
        [InlineData("00000000")]
        [InlineData("0000000000")]
        public void InvalidEik(string eik)
        {
            EikValidationAttribute attribute = new EikValidationAttribute();
            bool value = attribute.IsValid(eik);
            Assert.False(value);
        }
    }
}
