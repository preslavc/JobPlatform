namespace JobPlatform.Web.ViewModels.Administration.Dashboard
{
    using System;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class ReportViewModel : IMapFrom<Report>
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public int? JobPostId { get; set; }

        public string ApplicationUserId { get; set; }

        public bool Resolved { get; set; }

        public string ResolvedInfo { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
