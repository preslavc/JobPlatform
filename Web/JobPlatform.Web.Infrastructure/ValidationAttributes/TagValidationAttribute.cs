namespace JobPlatform.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using JobPlatform.Common;

    public class TagValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string tag = value.ToString();
            if (tag.Contains(" "))
            {
                string[] tags = tag.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

                if (tags.Length > 5)
                {
                    return new ValidationResult(GlobalConstants.ErrorMaxTags);
                }
            }

            return ValidationResult.Success;
        }
    }
}
