namespace JobPlatform.Web.ViewModels.Administration.Users
{
    using System.ComponentModel.DataAnnotations;

    using JobPlatform.Common;

    public class EditUserViewModel
    {
        public string UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = DisplayNameConstants.FirstName)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = DisplayNameConstants.LastName)]
        public string LastName { get; set; }

        [Display(Name = DisplayNameConstants.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
