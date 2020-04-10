namespace JobPlatform.Web.ViewModels.Jobs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using JobPlatform.Common;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;
    using JobPlatform.Web.Infrastructure.ValidationAttributes;

    public class EditViewModel : IMapFrom<JobPost>
    {
        public int Id { get; set; }

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

        public int EmployerId { get; set; }

        [Display(Name = DisplayNameConstants.JobTags)]
        [TagValidation]
        [MaxLength(100)]
        public string TagString { get; set; }
    }
}
