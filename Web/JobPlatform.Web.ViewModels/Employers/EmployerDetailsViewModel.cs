namespace JobPlatform.Web.ViewModels.Employers
{
    using System.Collections.Generic;
    using System.Linq;

    using Ganss.XSS;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;
    using JobPlatform.Web.ViewModels.Browse;

    public class EmployerDetailsViewModel : IMapFrom<Employer>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public ICollection<JobPostViewModel> JobPosts { get; set; }

        public IEnumerable<JobPostViewModel> JobPostsOrderByDescending
        {
            get => this.JobPosts.OrderByDescending(x => x.CreatedOn);
        }
    }
}
