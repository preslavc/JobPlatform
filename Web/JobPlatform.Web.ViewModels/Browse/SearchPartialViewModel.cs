namespace JobPlatform.Web.ViewModels.Browse
{
    using System.ComponentModel.DataAnnotations;

    public class SearchPartialViewModel
    {
        [Display(Prompt = "Keyword")]
        [MaxLength(50)]
        public string Keyword { get; set; }

        [Display(Prompt = "City")]
        [MaxLength(50)]
        public string City { get; set; }
    }
}
