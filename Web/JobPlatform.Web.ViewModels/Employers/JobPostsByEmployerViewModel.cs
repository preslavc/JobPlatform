namespace JobPlatform.Web.ViewModels.Employers
{
    using System;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class JobPostsByEmployerViewModel : IMapFrom<JobPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
