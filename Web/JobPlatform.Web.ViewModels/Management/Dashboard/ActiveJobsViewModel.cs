namespace JobPlatform.Web.ViewModels.Management.Dashboard
{
    using System;
    using System.Collections.Generic;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class ActiveJobsViewModel : IMapFrom<JobPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<CvMessage> CvMessages { get; set; }

        public int ApplyCounter => this.CvMessages.Count;
    }
}
