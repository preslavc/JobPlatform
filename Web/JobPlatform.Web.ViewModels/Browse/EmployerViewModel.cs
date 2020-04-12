namespace JobPlatform.Web.ViewModels.Browse
{
    using System.Collections.Generic;

    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class EmployerViewModel : IMapFrom<Employer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<JobPost> JobPosts { get; set; }

        public int JobPostCounter => this.JobPosts.Count;
    }
}
