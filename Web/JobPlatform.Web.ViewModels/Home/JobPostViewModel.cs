namespace JobPlatform.Web.ViewModels.Home
{
    using System;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class JobPostViewModel : IMapFrom<JobPost>
    {
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public Employer Employer { get; set; }
    }
}