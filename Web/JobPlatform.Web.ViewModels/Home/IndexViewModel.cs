namespace JobPlatform.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        [Display(Prompt = "Ключова дума")]
        [MaxLength(50)]
        public string Keyword { get; set; }

        [Display(Prompt = "Град")]
        [MaxLength(50)]
        public string City { get; set; }

        public IEnumerable<JobPostViewModel> JobPosts { get; set; }
    }
}
