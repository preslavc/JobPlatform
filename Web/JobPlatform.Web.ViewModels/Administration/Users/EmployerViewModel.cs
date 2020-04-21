namespace JobPlatform.Web.ViewModels.Administration.Users
{
    using System.ComponentModel.DataAnnotations;

    using JobPlatform.Common;
    using JobPlatform.Web.Infrastructure.ValidationAttributes;

    public class EmployerViewModel : UserViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = DisplayNameConstants.EmployerName)]
        public string EmployerName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [EikValidation]
        [Display(Name = DisplayNameConstants.Eik)]
        public string Eik { get; set; }
    }
}
