namespace JobPlatform.Web.ViewModels.Management.Dashboard
{
    using System.Collections.Generic;
    using System.Linq;

    public class IndexViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ActiveJobsViewModel> JobPosts { get; set; }

        public int ActiveJobs => this.JobPosts.Count();
    }
}
