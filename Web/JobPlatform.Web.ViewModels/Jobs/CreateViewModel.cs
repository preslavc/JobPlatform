namespace JobPlatform.Web.ViewModels.Jobs
{
    using System.ComponentModel.DataAnnotations;
    using JobPlatform.Common;
    using JobPlatform.Web.Infrastructure.ValidationAttributes;

    public class CreateViewModel
    {
        [Display(Name = DisplayNameConstants.JobTitle)]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = DisplayNameConstants.JobDescription)]
        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Country { get; set; }

        [Required]
        [MaxLength(20)]
        public string City { get; set; }

        [TagValidation]
        [MaxLength(100)]
        public string Tags { get; set; }
    }
}
