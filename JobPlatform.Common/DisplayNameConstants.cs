namespace JobPlatform.Common
{
    public static class DisplayNameConstants
    {
        public const string FirstName = "First name";

        public const string LastName = "Last name";

        public const string OldPassword = "Old password";

        public const string NewPassword = "New password";

        public const string ConfirmPassword = "Confirm password";

        public const string EmployerName = "Company name";

        public const string Eik = "EIK/BULSTAT";

        public const string PhoneNumber = "Phone number";

        public const string EmployerDescription = "Employer description";

        public const string CvFile = "CV file";

        public const string JobTitle = "Job title";

        public const string JobDescription = "Job description";

        public const string JobTags = "Tags";

        public const string ReportTitle = "Reason";

        public static readonly string[] ReportType = new string[]
        {
            "I think it's spam or a scam",
            "I think it's discriminatory or offensive",
            "I think something is broken or incorrect",
            "Other",
        };
    }
}
