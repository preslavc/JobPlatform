namespace JobPlatform.Web.ViewModels.Reports
{
    using System.ComponentModel.DataAnnotations;

    using JobPlatform.Common;

    public class ReportViewModel
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        [Display(Name = DisplayNameConstants.ReportTitle)]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Message { get; set; }
    }
}
