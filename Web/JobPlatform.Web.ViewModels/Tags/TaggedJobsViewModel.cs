namespace JobPlatform.Web.ViewModels.Tags
{
    using System;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;
    using JobPlatform.Web.ViewModels.Shared;

    public class TaggedJobsViewModel : IMapFrom<JobTag>
    {
        public int JobPostId { get; set; }

        public JobPostViewModel JobPost { get; set; }

        public int Id => this.JobPost.Id;

        public string Title => this.JobPost.Title;

        public string City => this.JobPost.City;

        public string Country => this.JobPost.Country;

        public DateTime CreatedOn => this.JobPost.CreatedOn;

        public Employer Employer => this.JobPost.Employer;
    }
}
