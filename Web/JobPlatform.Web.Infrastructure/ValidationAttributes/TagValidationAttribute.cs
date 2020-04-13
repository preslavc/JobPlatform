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
                    return new ValidationResult(ErrorMessageConstants.ErrorMaxTags);
                }

                foreach (var t in tags)
                {
                    if (t.Length > 20)
                    {
                        return new ValidationResult(ErrorMessageConstants.ErrorMaxLengthTag);
                    }
                }
            }
            else
            {
                if (tag.Length > 20)
                {
                    return new ValidationResult(ErrorMessageConstants.ErrorMaxLengthTag);
                }
            }

            return ValidationResult.Success;
        }
    }
}
