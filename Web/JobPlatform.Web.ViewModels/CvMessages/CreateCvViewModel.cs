namespace JobPlatform.Web.ViewModels.CvMessages
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateCvViewModel
    {
        public int PostId { get; set; }

        [Display(Name = "Име")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Display(Name = "Съобщение/Мотивационно писмо")]
        public string Message { get; set; }

        [Required]
        public IFormFile CvFile { get; set; }
    }
}
