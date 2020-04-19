namespace JobPlatform.Web.ViewModels.Administration.Reports
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class ReportViewModel : IMapFrom<Report>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public int? JobPostId { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name="Status")]
        public bool Resolved { get; set; }

        [Display(Name = "Information")]
        public string ResolvedInfo { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
