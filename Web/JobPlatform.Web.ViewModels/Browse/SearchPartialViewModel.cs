namespace JobPlatform.Web.ViewModels.Browse
{
    using System.ComponentModel.DataAnnotations;

    public class SearchPartialViewModel
    {
        [Display(Prompt = "Ключова дума")]
        [MaxLength(50)]
        public string Keyword { get; set; }

        [Display(Prompt = "Град")]
        [MaxLength(50)]
        public string City { get; set; }
    }
}
