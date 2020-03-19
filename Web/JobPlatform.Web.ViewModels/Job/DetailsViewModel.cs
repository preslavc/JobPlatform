namespace JobPlatform.Web.ViewModels.Job
{
    using System;
    using System.Collections.Generic;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class DetailsViewModel : IMapFrom<JobPost>
    {
        public string Title { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public Employer Employer { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Date => this.CreatedOn.ToString("dd.MM.yyyy");

        public string Description { get; set; }

        //public IEnumerable<string> Tags { get; set; }
    }
}
