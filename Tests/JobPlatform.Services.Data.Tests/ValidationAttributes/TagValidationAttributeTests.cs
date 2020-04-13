namespace JobPlatform.Services.Data.Tests.ValidationAttributes
{
    using JobPlatform.Web.Infrastructure.ValidationAttributes;
    using Xunit;

    public class TagValidationAttributeTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("java")]
        [InlineData("java web")]
        [InlineData("java web c# python test")]
        [InlineData("tagwithlongname67890")]
        public void ValidTags(string tags)
        {
            TagValidationAttribute attribute = new TagValidationAttribute();
            bool value = attribute.IsValid(tags);
            Assert.True(value);
        }

        [Theory]
        [InlineData("java web c# javascript python test")]
        [InlineData("tagwithlongname678901")]
        [InlineData("tagwithlongname678901 test")]
        public void InvalidTags(string tags)
        {
            TagValidationAttribute attribute = new TagValidationAttribute();
            bool value = attribute.IsValid(tags);
            Assert.False(value);
        }
    }
}
