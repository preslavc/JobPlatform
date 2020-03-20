namespace JobPlatform.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    using JobPlatform.Common;

    public class EikValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string eik = value.ToString();
            if (!Regex.IsMatch(eik, "^(?=[0-9]*$)(?:.{9}|.{13})$"))
            {
                return new ValidationResult(GlobalConstants.ErrorMessageInvalidEik);
            }

            if (eik.Length == 9)
            {
                return this.CalculateNineDigitEik(eik) ? ValidationResult.Success : new ValidationResult(GlobalConstants.ErrorMessageInvalidEik);
            }

            if (eik.Length == 13)
            {
                return this.CalculateThirteenDigitEik(eik) ? ValidationResult.Success : new ValidationResult(GlobalConstants.ErrorMessageInvalidEik);
            }

            return new ValidationResult(GlobalConstants.ErrorMessageInvalidEik);
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

        private bool CalculateThirteenDigitEik(string eik)
        {
            int[] weight1 = new int[] { 2, 7, 3, 5 };
            int[] weight2 = new int[] { 4, 9, 5, 7 };

            int checksum = 0;

            for (int i = 8; i < 12; i++)
            {
                checksum += (eik[i] - '0') * weight1[i - 8];
            }

            int remainder = checksum % 11;
            if (remainder != 10)
            {
                return remainder == eik[12] - '0';
            }

            checksum = 0;
            for (int i = 8; i < 12; i++)
            {
                checksum += (eik[i] - '0') * weight2[i - 8];
            }

            remainder = (checksum % 11) % 10;
            return remainder == eik[12] - '0';
        }
    }
}
