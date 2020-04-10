namespace JobPlatform.Web.ViewModels.CvMessages
{
    using System.ComponentModel.DataAnnotations;
    using JobPlatform.Common;
    using Microsoft.AspNetCore.Http;

    public class CreateCvViewModel
    {
        public int PostId { get; set; }

        [Display(Name = DisplayNameConstants.FirstName)]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = DisplayNameConstants.LastName)]
        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        [Display(Name = DisplayNameConstants.CvFile)]
        [Required]
        public IFormFile CvFile { get; set; }
    }
}
