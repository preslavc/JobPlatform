namespace JobPlatform.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    using JobPlatform.Common;

    public class EikValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessageConstants.ErrorMessageInvalidEik);
            }

            string eik = value.ToString();
            if (!Regex.IsMatch(eik, "^[0-9]{9}$"))
            {
                return new ValidationResult(ErrorMessageConstants.ErrorMessageInvalidEik);
            }

            return this.CalculateNineDigitEik(eik) ? ValidationResult.Success : new ValidationResult(ErrorMessageConstants.ErrorMessageInvalidEik);
        }

        private bool CalculateNineDigitEik(string eik)
        {
            int checksum = 0;
            for (int i = 0; i < 8; i++)
            {
                checksum += (eik[i] - '0') * (i + 1);
            }

            int remainder = checksum % 11;
            if (remainder != 10)
            {
                return remainder == eik[8] - '0';
            }

            checksum = 0;
            for (int i = 0; i < 8; i++)
            {
                checksum += (eik[i] - '0') * (i + 3);
            }

            remainder = (checksum % 11) % 10;
            return remainder == eik[8] - '0';
        }
    }
}
