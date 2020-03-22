namespace JobPlatform.Web.ViewModels.Job
{
    using System.ComponentModel.DataAnnotations;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class EditViewModel : IMapFrom<JobPost>
    {
        public int Id { get; set; }

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

        public int EmployerId { get; set; }
    }
}
