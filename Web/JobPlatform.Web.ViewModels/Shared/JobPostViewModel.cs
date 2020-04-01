namespace JobPlatform.Web.ViewModels.Shared
{
    using System;
    using System.Collections.Generic;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class JobPostViewModel : IMapFrom<JobPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; }

        public Employer Employer { get; set; }

        public IEnumerable<JobTag> Tags { get; set; }
    }
}
