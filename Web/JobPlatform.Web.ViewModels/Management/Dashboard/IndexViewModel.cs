namespace JobPlatform.Web.ViewModels.Management.Dashboard
{
    using System.Collections.Generic;

    using JobPlatform.Web.ViewModels.Management.Jobs;

    public class IndexViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ActiveJobsViewModel> JobPosts { get; set; }

        public int ActiveJobs { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
