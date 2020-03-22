namespace JobPlatform.Web.ViewModels.Jobs
{
    using System.ComponentModel.DataAnnotations;

    public class CreateViewModel
    {
        [Display(Name = "Позиция")]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Държава")]
        [Required]
        [MaxLength(20)]
        public string Country { get; set; }

        [Display(Name = "Град")]
        [Required]
        [MaxLength(20)]
        public string City { get; set; }
    }
}
